using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	public abstract partial class BaseRound : BaseNetworkable
	{
		public virtual int RoundDuration => 0;

		public List<Player> Players = new();

		public float RoundEndTime { get; set; }

		public float TimeLeft
		{
			get
			{
				return RoundEndTime - Sandbox.Time.Now;
			}
		}
		public void Start()
		{
			if ( Host.IsServer && RoundDuration > 0 )
				RoundEndTime = Sandbox.Time.Now + RoundDuration;
			OnStart();
		}
		public void Finish()
		{
			if ( Host.IsServer )
			{
				RoundEndTime = 0f;
				Players.Clear();
			}

			OnFinish();
		}
		public virtual void OnPlayerLeave( Player player )
		{
			Players.Remove( player );
		}

		public void AddPlayer( Player player )
		{
			Host.AssertServer();

			if ( !Players.Contains( player ) )
				Players.Add( player );
		}

		public virtual void OnTick() { }

		protected virtual void OnStart() { }
		protected virtual void OnFinish() { }
		protected virtual void OnTimeUP() { }
	}
}
