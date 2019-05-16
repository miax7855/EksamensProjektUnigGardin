using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
	public interface IOnStockUpdatedSubscriber
	{
		void OnStockUpdated(object sender, EventArgs e);
	}
}
