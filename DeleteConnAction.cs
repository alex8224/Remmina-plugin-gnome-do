// 
//  DeleteConnAction.cs
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
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Do.Universe;
namespace Remmina
{
	public class DeleteConnAction:Act
	{
		
		public override string Name {
			get {
				return "删除Remmina主机";
			}
		}

		public override string Description {
			get {
				return "注意，将会删除Remmina配置文件";
			}
		}

		public override string Icon {
			get {
				return Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location),"remmina.png"); 
			}
		}

		public override IEnumerable<Type> SupportedItemTypes {
			get {
				yield return typeof(RemminaItem);
			}
		}

		public override bool SupportsItem (Item item)
		{
			return item is RemminaItem;
		}

		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
		{
			(items.First () as RemminaItem).DeleteConnection ();
			yield break;
		}
	}
}

