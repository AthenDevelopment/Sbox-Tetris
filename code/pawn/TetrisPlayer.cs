using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Tetris.UI;
namespace Tetris
{
	public partial class TetrisPlayer : Player
	{
		//[Net] public TetrisBlock Block { get; set; }
		public TimeSince TimeSinceSpawn { get; private set; }
		[Net] public int PlayerID { get; set; } = -1;
		[Net] public BaseRound baseRound { get; set; }


		public TetrisPlayer()
		{
			
		Camera = new TetrisCamera();
		

		}
		public override void Respawn()
		{
			//Camera = new TetrisCamera();

			base.Respawn();
		}
		//[Event( "tetris.CreateBlockAfterTouch" )]
		public void CreateBlockAfterTouch(Client cl)
		{
			Log.Info( "Ran tetris.CreateBlockAfterTouch" );
			//if ( !Host.IsServer ) return;
			
				if ( TimeSinceSpawn > 3 )
				{
					var GetSpawnPoint = TetrisBlockSpawn.All.ToList().Find( e => e.Owner == cl.Pawn.Owner );
				//Log.Info( GetSpawnPoint );
				//var GetSpawnPoint = Entity.All.OfType<TetrisBlockSpawn>().ToList();
				var block = new TetrisBlock
					{
						Position = GetSpawnPoint.Position.SnapToGrid( 64.0f, true, true, true ),
					};

					block.PhysicsGroup.Mass = 5f;
					block.Tags.Add( "InPlayerUse" );

					cl.Pawn = block;
					TimeSinceSpawn = 0;

				}
				
		}

		
		
	}
}
