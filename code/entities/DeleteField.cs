using Sandbox;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	[Library( "Delete_Field" )]
	public partial class TetrisDeleteField : BaseTrigger
	{
		[Property]
		public TetrisBlockType Type { get; set; }
	}
}
