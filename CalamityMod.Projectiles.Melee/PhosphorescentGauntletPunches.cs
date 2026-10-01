using System;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PhosphorescentGauntletPunches : ModProjectile, ILocalizedModType, IModType
{
	public const float LungeSpeed = 19f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool HasPerformedLunge
	{
		get
		{
			return base.Projectile.ai[0] == 1f;
		}
		set
		{
			int newValue = value.ToInt();
			if (base.Projectile.ai[0] != (float)newValue)
			{
				base.Projectile.ai[0] = newValue;
				base.Projectile.netUpdate = true;
			}
		}
	}

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 14;
	}

	public override void SetDefaults()
	{
		base.Projectile.scale = 1.6f;
		base.Projectile.width = (base.Projectile.height = (int)(base.Projectile.scale * 60f));
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
		base.Projectile.frameCounter = 0;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (!HasPerformedLunge)
		{
			PerformLunge();
		}
		Vector2 topLeft = base.Projectile.Center + base.Projectile.velocity.RotatedBy(-1.5707963705062866) * 40f;
		Vector2 topRight = base.Projectile.Center + base.Projectile.velocity.RotatedBy(1.5707963705062866) * 40f;
		if (Time >= 8f && !Collision.CanHitLine(topLeft, 8, 8, topRight, 8, 8))
		{
			ReelBack();
		}
		HandleProjectileVisuals();
		HandlePositioning();
		Time++;
	}

	internal void PerformLunge()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Owner.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 19f;
			HasPerformedLunge = true;
		}
	}

	internal void ReelBack()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		Owner.GiveUniversalIFrames(15);
		if (!Main.dedServ)
		{
			Vector2 topLeft = base.Projectile.Center + base.Projectile.velocity.RotatedBy(-1.5707963705062866) * 40f;
			Vector2 top = base.Projectile.Center + base.Projectile.velocity * 70f;
			Vector2 topRight = base.Projectile.Center + base.Projectile.velocity.RotatedBy(1.5707963705062866) * 40f;
			foreach (Vector2 point in new BezierCurve(topLeft, top, topRight).GetPoints(50))
			{
				Dust dust = Dust.NewDustPerfect(point + base.Projectile.velocity * 16f, 75);
				dust.velocity = base.Projectile.velocity * 4f;
				dust.noGravity = true;
				dust.scale = 1.2f;
			}
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			Owner.velocity = Vector2.Reflect(Owner.velocity.SafeNormalize(Vector2.Zero), base.Projectile.velocity.SafeNormalize(Vector2.Zero)) * ((Vector2)(ref Owner.velocity)).Length();
			Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width + 16, base.Projectile.height + 16);
			base.Projectile.Kill();
		}
	}

	internal static void GenerateDustOnOwnerHand(Player player)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		Vector2 handOffset = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
		if (player.direction != 1)
		{
			handOffset.X = (float)player.bodyFrame.Width - handOffset.X;
		}
		if (player.gravDir != 1f)
		{
			handOffset.Y = (float)player.bodyFrame.Height - handOffset.Y;
		}
		handOffset -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - player.height)) / 2f;
		Vector2 rotatedHandPosition = player.RotatedRelativePoint(player.position + handOffset, reverseRotation: true);
		for (int i = 0; i < 4; i++)
		{
			Dust dust = Dust.NewDustDirect(player.Center, 0, 0, 75, 0f, 0f, 150, default(Color), 1.3f);
			dust.position = rotatedHandPosition;
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			dust.fadeIn = 1f;
			dust.velocity += player.velocity;
			if (Main.rand.NextBool())
			{
				dust.position += Utils.RandomVector2(Main.rand, -4f, 4f);
				dust.scale += Main.rand.NextFloat();
			}
		}
	}

	internal void HandleProjectileVisuals()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		float velocityAngle = base.Projectile.velocity.ToRotation();
		base.Projectile.rotation = velocityAngle + (float)Math.PI;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 3 == 2)
		{
			base.Projectile.frame++;
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.Kill();
			}
		}
	}

	internal void HandlePositioning()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter);
		Projectile projectile = base.Projectile;
		projectile.Center += base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 30f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, effects: (SpriteEffects)(base.Projectile.spriteDirection == -1), texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: lightColor, rotation: base.Projectile.rotation, scale: base.Projectile.scale);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ReelBack();
	}
}
