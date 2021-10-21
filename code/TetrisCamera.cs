using Sandbox;

namespace Tetris
{
	public partial class TetrisCamera : FixedCamera
	{
		[Net] public TetrisArena TetrisArena { get; set; }
		[Net] public Vector3 TargetPos { get; set; }
		[Net] public Rotation TargetRot { get; set; } = Rotation.From( 0, 0, 0 );

		public override void Update()
		{
			var Pawn = Local.Pawn;
			
			FieldOfView = 70;
			Pos = TetrisArena.CameraPos;
			Rot = TargetRot;
			Viewer = null;

		}
	}

}
