using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tetris;
namespace Tetris
{

	public partial class PlayRound : BaseRound
	{
		[Net] public TetrisPlayer TetrisPlayer { get; set; }
		public override void OnPlayerJoin(Player player)
		{
			
		}
		protected override void OnStart()
		{





			Event.Run( "tetris.CreateBlock");




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
