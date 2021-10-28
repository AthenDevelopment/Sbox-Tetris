using Sandbox;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.entities
{
	[Library( "Delete_Field" )]
	[Hammer.Model]
	public partial class TetrisDeleteField : BaseTrigger
	{
		public TetrisDeleteField()
		{
			EnableAllCollisions = true;
			EnableHitboxes = true;
		}
		public override void Spawn()
		{
			
			SetupPhysicsFromModel( PhysicsMotionType.Static, false );
			Transmit = TransmitType.Always;
			base.Spawn();
		}
	}
}
