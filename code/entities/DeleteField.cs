using Sandbox;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	[Library( "Delete_Field" )]
	public partial class TetrisDeleteField : ModelEntity
	{
		public TetrisDeleteField()
		{
			EnableAllCollisions = true;
			EnableHitboxes = true;
		}
		public override void Spawn()
		{
			
			//SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
			Transmit = TransmitType.Always;
			base.Spawn();
		}
		public override void Touch( Entity other )
		{



			base.Touch( other );
		}
	}
}
