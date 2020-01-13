using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESSim
{
	class Player
	{
		public string Username;
		public char SideChosen;
		
		public Player(string name, char side)
		{
			Username = name;
			SideChosen = side;
		}
	}
}
