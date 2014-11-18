// 
//  NewConnAction.cs
//  
//  Author:
//       Alex Zhang <alex8224@gmail.com>
// 
//  Copyright © 2014 Alex Zhang <alex8224@gmail.com>
// 
//  This library is free software; you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 2.1 of the
//  License, or (at your option) any later version.
// 
//  This library is distributed in the hope that it will be useful, but
//  WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
using System;
using System.IO;
using Do.Universe;
using System.Collections.Generic;

namespace Remmina
{
	public class NewConnAction:Act
	{
		public override string Name {
			get {
				return "创建新主机";
			}
		}

		public override string Description {
			get {
				return Name;
			}
		}

		public override string Icon {
			get {
				return Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location),"remmina.png"); 
			}
		}

		public override IEnumerable<Type> SupportedItemTypes {
			get {
				yield return typeof(Item);
			}
		}

		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
		{
			RemminaItem.NewConnection ();
			yield break;
		}
	}
}

