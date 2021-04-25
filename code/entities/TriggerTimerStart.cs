using Sandbox;
using Strafe.Ply;

namespace Strafe.Entities
{

	[Library( "trigger_timer_start" )]
	public partial class TriggerTimerStart : ModelEntity
	{

		public override void Spawn()
		{
			base.Spawn();

			SetupPhysicsFromModel( PhysicsMotionType.Static );
			CollisionGroup = CollisionGroup.Trigger;
			EnableSolidCollisions = false;
			EnableTouch = true;
			EnableTouchPersists = true;

			Transmit = TransmitType.Never;
		}

		public override void StartTouch( Entity other )
		{
			base.StartTouch( other );
		}

		public override void EndTouch( Entity other )
		{
			base.EndTouch( other );

			if ( other is not StrafePlayer player )
			{
				return;
			}

			player.Time = 0;
		}

	}

}
