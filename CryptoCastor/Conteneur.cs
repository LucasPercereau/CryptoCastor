using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCastor
{
	class Conteneur
	{
		private string titre;
		private string passwd;
		private PassWordValider pwv;

		public Conteneur(string t, string p)
		{
			pwv = new PassWordValider();
			pwv.validate(p);

			if (t == "")
			{
				throw new System.ArgumentException("Title cannot be null");
			}

			titre = t;
			passwd = p;
		}

		public string Titre
		{
			get { return titre; }
			set { titre = value; }
		}
		public string Pass
		{
			get { return passwd; }
			set { passwd = value; }
		}

		public override string ToString()
		{
			return Titre + " : "+ Pass +".";  
		}
	}
}
