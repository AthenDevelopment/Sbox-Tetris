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
		[Net] public TetrisBlock Block { get; set; }
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
		[Event( "tetris.CreateBlockNext" )]
		public void CreateBlockAfterTouch(Client cl)
		{
				if ( TimeSinceSpawn > 3 )
				{
					var GetSpawnPoint = Entity.All.OfType<TetrisBlockSpawn>().ToList().Find( e => e.Owner == cl.Pawn.Owner );
					Log.Info( GetSpawnPoint );
					var block = new TetrisBlock
					{
						Position = GetSpawnPoint.Position.SnapToGrid( 64.0f, true, true, true ),
					};

					block.PhysicsGroup.Mass = 5f;
					block.Tags.Add( "InPlayerUse" );

					GetSpawnPoint.Client.Pawn = block;
					TimeSinceSpawn = 0;

				}
		}
		
		
	}
}
