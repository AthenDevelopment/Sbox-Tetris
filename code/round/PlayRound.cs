using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tetris;
using Tetris.Extensions;
namespace Tetris
{

	public partial class PlayRound : BaseRound
	{
		public override void OnPlayerJoin(Player player)
		{
			
		}
		protected override void OnStart()
		{
			if(Host.IsServer)
			{
				var spawns = Entity.All.OfType<TetrisBlockSpawn>().ToList();
				var players = Client.All.Select( ( client ) => client.Pawn as Player ).ToList();

				foreach ( var player in players )
				{
					Log.Info( "Ran OnStart" );

					if ( spawns.Count > 0 )
					{
						var spawn = spawns[0];
						spawns.RemoveAt( 0 );
						Log.Info( "Count: " + spawns.Count );
						spawn.Owner = player.Owner;
						Log.Info( player.Owner );
						if ( spawn.Owner == player.Owner )
						{
							var block = new TetrisBlock
							{
								Position = spawn.Position.SnapToGrid( 64.0f, true, true, true ),
							};

							block.PhysicsGroup.Mass = 5f;
							block.Tags.Add( "InPlayerUse" );

							player.Client.Pawn = block;
							
							AddPlayer( player );
						}
						



						
					}
					else
					{
						Log.Error( "Error" );
					}
				}
			}
			




			//StartGame();
		base.OnStart();
		}
		private void StartGame()
		{
			var spawns = Entity.All.OfType<TetrisBlockSpawn>().ToList();
			var players = Client.All.Select( ( client ) => client.Pawn as Player ).ToList();

			foreach(var player in players)
			{
				Event.Run( "tetris.CreateBlock", player.Client, player.Client.UserId );
				Log.Info( player );
				
			}
		}
		protected override void OnFinish()
		{

		}
	}
}
