using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MainCharacter
{
	public class RotatingList<T> : Collection<T> {
		int selectedIndex;
		
		public RotatingList() : base(){
			selectedIndex = 0;
		}
		
		public RotatingList(IList<T> coll) : base(coll){}
		
		public T Next(){
			selectedIndex = ++selectedIndex % Count;
			return this[selectedIndex];
		}
		
		public T Previous(){
			selectedIndex = selectedIndex - 1 < 0 ? Count-1 : selectedIndex - 1;
			return this[selectedIndex];
		}
	}
}

