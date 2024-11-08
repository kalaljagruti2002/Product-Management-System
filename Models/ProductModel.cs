using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Image_Project.Models
{
	public class ProductModel
	{
		public int ProdId { get; set; }
		public string ProdName { get; set; }
		public Nullable<int> ProdRate { get; set; }
		public Nullable<int> ProdQty { get; set; }
		public string ProdTotal { get; set; }
		public string ProdImage { get; set; }
		public Nullable<System.DateTime> Date { get; set; }
	}
}