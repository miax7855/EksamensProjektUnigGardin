using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;
using library;

namespace ApplicationLayer
{
	// overflødig, en delegate indeholder reference metoder (liste over subscribers(metoder))
	// Metoderne skal være af samme layout
	public delegate void RegisterOrderEventHandler<OrderRepository>(object source, EventArgs e);

    public class ImportController
    {
		// event og eventhandler
        public event EventHandler<OrderRepository> OrderRegistered;

        private string[] orderItems;
		private string[] sampleTypeArray;
        private string[] dateTimeArray;
		private List<string> sampleTypeList;
        
        private OrderRepository orderRepo;
		
		public void RegisterOrders(object fileNameObj)
		{
			// FilePath findes
            string fileName = (string)fileNameObj;
            string relativePath = GetFilePath(fileName);
            List<IOrder> newOrders = new List<IOrder>();
			// OrderRepo hentes
            orderRepo = OrderRepository.GetOrderRepo();
			// instans af fileStream, skaber en åben strøm til en fil, strømmen kan defineres vha. nedenstående parametre
			// FileAccess sat til Read så vi kan læse i filen. FileMode sat til Open så filen åbnes
			// Fileshare. "ReadWrite" er nu overflødig pga. der ikke længere skrives i txt. filen
			// relativepath er fillokationen
            FileStream fs = new FileStream(relativePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

			// for at læse FileStream, benyttes der StreamReader med FileStream som input.
            using (StreamReader reader = new StreamReader(fs))
            {
				// kører indtil programmet lukkes, implementeret pga. der skal tjekkes løbende for opdateringer til txt. filen
				// Metoden skal kaldes via en thread, ellers går programmet i stå. Metoden bliver kun udført hvis der er en linje
				// i tekstfilen som kan læses.
                while (true)
                {
                    string line = string.Empty;
                    // Hvis der er mere tekst i txt filen fortsætter metoden.
                    while ((line = reader.ReadLine()) != null)
                    {
                        orderItems = line.Split(';');
						// teksten fra txt.filen indsættes i ordre objekter som data i forhold til syntaksen i txt.filen
                        dateTimeArray = orderItems[6].Split(',');
                        sampleTypeArray = orderItems[7].Split(',');
                        sampleTypeList = ConvertArrayToList(sampleTypeArray);
                        
                        DateTime timeStamp = new DateTime  (Convert.ToInt32(dateTimeArray[0]), Convert.ToInt32(dateTimeArray[1]), 
                                                            Convert.ToInt32(dateTimeArray[2]), Convert.ToInt32(dateTimeArray[3]), 
                                                            Convert.ToInt32(dateTimeArray[4]), Convert.ToInt32(dateTimeArray[5]));

                        Order order = new Order(orderItems[0], orderItems[1], Convert.ToInt32(orderItems[2]), orderItems[3], 
                                                 Convert.ToInt32(orderItems[4]), orderItems[5], timeStamp, sampleTypeList);
						// tilføjer ordren til en kollektion, en intern liste i klassen.
                        newOrders.Add(order);
                    }
					// Hvis der eksisterer indlæste ordre kører følgende if statement
                    if (newOrders.Count != 0)
                    {
						// tømmer liste
                        orderRepo.GetListOfOrdersToAdd().Clear();
						// Sammenligner 2 kollektioner hvor objekter i difference indsættes i "GetListOfOrdersToAdd" 
						// vha. "AddRange" som er en addering af kollektioner, hvor "Except" metoden sammenligner objekterne
                        orderRepo.GetListOfOrdersToAdd().AddRange(newOrders.Except(orderRepo.ReturnOrdersAsList()));
                        // Indsætter alle nye ordre i Dictionary
                        foreach (IOrder item in orderRepo.GetListOfOrdersToAdd())
                        {
                            if (orderRepo.GetOrderDic().Count != 0)
                            {
                                int id = orderRepo.GetOrderDic().Keys.Last() + 1;
                                item.OrderId = id;
                                orderRepo.AddOrder(id, item);
                            }
							// hvis databasen er tom, starter ID fra 1000.
                            else
                            {
                                orderRepo.AddOrder(1000, item);
                            }
                        }
						// Event bliver kaldt
                        OnOrderRegistered();
						// Kollektion tømmes
                        newOrders.Clear();
                    }
                }
            }
        }
		// Åbner tekstfil og sletter alt indhold. Bruges til test hvor der skrives i tekstfiler
		// derfor benyttes der Truncate (tømmer filen) ReadWrite er overflødig
        public void DeleteOrderItemEvent(object fileNameObj)
        {
            string fileName = (string)fileNameObj;
			string relativePath = GetFilePath(fileName);
            FileStream filestream = new FileStream(relativePath, FileMode.Truncate, FileAccess.ReadWrite);
			filestream.Close();
        }
		// Metoder er overflødig, bør slettes.
		// Append åbner filen og sættes til slutningen af tekstfilen, Skal have en write for at kunne skrive i filen.
		// FileStream kan kun benytte bytes som input. Derfor laves en string om til et byte Array.
		// Encoding klassen kan konvertere en string til byte array. Metoden kan kun konvertere en linje ad gangen.
		// FileStream.Write parameter: ( input til skrivning, hvor skal der starte ( 0 er nuværende position pga. Append )
		// Buffer.Length er længden på bytes der indsættes, da metoden skal vide dette på forhånd.
		// FileStream.Flush(); fungerer eksekverer skrivningen.
        public void RegisterOrdersInGUI(object fileNameObj, string orderLinesToAdd)
        {
            string filename = (string)fileNameObj;
            FileStream filestream = new FileStream(filename, FileMode.Append, FileAccess.Write);
            byte[] buffer = Encoding.Default.GetBytes(orderLinesToAdd);
            filestream.Write(buffer, 0, buffer.Length);
            filestream.Flush();
            filestream.Close();
        }
		// kalder alle nuværende registrede subscribers
		// eventet kalder på 2 metoder. "SaveOrder" i Databasen samt "ShowOrderIDsInListBox" i GUI'en.
		public void OnOrderRegistered()
        {
            if (OrderRegistered != null)
            {
                OrderRegistered(this, OrderRepository.GetOrderRepo());
            }
        }
        // konverterer et array til liste, fungerer kun ved string collections
        public List<string> ConvertArrayToList(string[] c)
		{
			List<string> ListConvert = new List<string>();
			foreach (string item in c)
			{
				ListConvert.Add(item);
			}

			return ListConvert;
		}
		// returnerer en liste.
		public List<string> GetSampleTypeList()
		{
			return sampleTypeList;
		}
		// Metode der giver en filePath til en specifik mappe. Fungerer ved forskellige PC'er pga. brug af Environment
		// Environment.CurrentDictory giver filePath til hvor programmet befinder sig.
		// herefter benyttes "GetParent" for at finde ovenstående mappe
		// endelig fil destination er hard kodet.
		public string GetFilePath(string fileName)
		{
			string destination = Environment.CurrentDirectory;

			for (int i = 0; i < 3; i++)
			{
				destination = Directory.GetParent(destination).ToString();
			}

			return destination + "/Domainlayer/Content/" + fileName;
		}
	}
}
