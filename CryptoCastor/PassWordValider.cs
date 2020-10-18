using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCastor
{
	class PassWordValider
	{
		public PassWordValider()
		{

		}

		public void validate(string pass)
		{
			if(pass=="")
			{
				throw new System.ArgumentException("Pass cannot be null");
			}
			string[] sTab = pass.Split(' ');
			if(sTab.Length>1)
			{
				throw new System.ArgumentException("No space in pass !");
			}
			if (pass.Length > 25)
			{
				throw new System.ArgumentException("Pass length must be less than 25");
			}
		}

		public void validateGenerale(string pass)
		{
			if (pass == "")
			{
				throw new System.ArgumentException("Pass cannot be null");
			}
			string[] sTab = pass.Split(' ');
			if (sTab.Length > 1)
			{
				throw new System.ArgumentException("No space in pass !");
			}
			if (pass.Length > 15)
			{
				throw new System.ArgumentException("Pass length must be less than 15");
			}
			if (pass.Length < 6)
			{
				throw new System.ArgumentException("Pass length must be at least 6");
			}
		}
	}
}
