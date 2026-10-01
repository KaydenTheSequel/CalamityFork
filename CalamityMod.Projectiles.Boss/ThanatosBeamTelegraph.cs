using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ThanatosBeamTelegraph : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 180;

	public const float TelegraphWidth = 3600f;

	public const float BeamPosOffset = 16f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public NPC ThingToAttachTo
	{
		get
		{
			if (!Main.npc.IndexInRange((int)base.Projectile.ai[0]))
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[0]];
		}
	}

	public float ConvergenceRatio => MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(25f, 120f, Time, clamped: true));

	public ref float StartingRotationalOffset => ref base.Projectile.ai[1];

	public ref float ConvergenceAngle => ref base.Projectile.localAI[0];

	public ref float Time => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(ConvergenceAngle);
		writer.Write(Time);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		ConvergenceAngle = reader.ReadSingle();
		Time = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		if (ThingToAttachTo == null || !ThingToAttachTo.active || ThingToAttachTo.Calamity().newAI[0] != 2f)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 hostNPCDirection = Vector2.Normalize(ThingToAttachTo.velocity);
		float beamStartForwardsOffset = -8f;
		base.Projectile.Center = ThingToAttachTo.Center;
		Projectile projectile = base.Projectile;
		projectile.position += hostNPCDirection * 16f + new Vector2(0f, 0f - ThingToAttachTo.gfxOffY);
		Projectile projectile2 = base.Projectile;
		projectile2.position += hostNPCDirection * beamStartForwardsOffset;
		base.Projectile.rotation = StartingRotationalOffset.AngleLerp(ConvergenceAngle, ConvergenceRatio) + ThingToAttachTo.velocity.ToRotation();
		Time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		Texture2D laserTelegraph = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/LaserWallTelegraphBeam", (AssetRequestMode)2).Value;
		float verticalScale = Utils.GetLerpValue(0f, 20f, Time, clamped: true) * Utils.GetLerpValue(0f, 16f, base.Projectile.timeLeft, clamped: true) * 4f;
		Vector2 origin = laserTelegraph.Size() * new Vector2(0f, 0.5f);
		Vector2 scaleInner = default(Vector2);
		((Vector2)(ref scaleInner))._002Ector(3600f / (float)laserTelegraph.Width, verticalScale);
		Vector2 scaleOuter = scaleInner * new Vector2(1f, 2.2f);
		Color colorOuter = Color.Lerp(Color.Red, Color.CornflowerBlue, Time / 180f);
		colorOuter = Color.Lerp(colorOuter, Color.White, Utils.GetLerpValue(40f, 0f, base.Projectile.timeLeft, clamped: true) * 0.8f);
		Color colorInner = Color.Lerp(colorOuter, Color.White, 0.5f);
		colorInner *= 0.85f;
		colorOuter *= 0.7f;
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorOuter, base.Projectile.rotation, origin, scaleOuter, (SpriteEffects)0);
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorInner, base.Projectile.rotation, origin, scaleInner, (SpriteEffects)0);
		return false;
	}
}
