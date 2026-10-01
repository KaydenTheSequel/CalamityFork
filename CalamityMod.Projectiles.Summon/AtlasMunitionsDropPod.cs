using System.Collections.Generic;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AtlasMunitionsDropPod : ModProjectile, ILocalizedModType, IModType
{
	public const float Gravity = 1.1f;

	public const float MaxFallSpeed = 24f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public float TileCollisionYThreshold => base.Projectile.ai[0];

	public bool HasCollidedWithGround
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public ref float SquishFactor => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 14;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 86;
		base.Projectile.height = 130;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.sentry = true;
	}

	public override void AI()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		if (SquishFactor <= 0f)
		{
			SquishFactor = 1f;
		}
		if (base.Projectile.velocity.Y == 0f && !HasCollidedWithGround)
		{
			PerformGroundCollisionEffects();
			HasCollidedWithGround = true;
			base.Projectile.netUpdate = true;
		}
		SquishFactor = MathHelper.Lerp(SquishFactor, 1f, 0.08f);
		base.Projectile.tileCollide = base.Projectile.Bottom.Y >= TileCollisionYThreshold;
		base.Projectile.frameCounter++;
		if (!HasCollidedWithGround)
		{
			base.Projectile.frame = base.Projectile.frameCounter / 6 % 5;
		}
		else
		{
			base.Projectile.velocity.X = 0f;
			if (base.Projectile.frame < 5)
			{
				base.Projectile.frame = 5;
			}
			if (base.Projectile.frameCounter % 8 == 7)
			{
				base.Projectile.frame++;
				if (base.Projectile.frame == 8)
				{
					SoundEngine.PlaySound(in ThanatosHead.VentSound, base.Projectile.Top);
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromAI(), base.Projectile.Center + Vector2.UnitY * 10f, Vector2.Zero, ModContent.ProjectileType<AtlasMunitionsAutocannon>(), base.Projectile.damage, 0f, base.Projectile.owner);
						projectile.originalDamage = base.Projectile.originalDamage;
						projectile.ai[2] = base.Projectile.whoAmI;
						Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Top + Vector2.UnitY * 72f, Vector2.Zero, ModContent.ProjectileType<AtlasMunitionsDropPodUpper>(), 0, 0f, base.Projectile.owner);
					}
				}
			}
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.frame = Main.projFrames[base.Type] - 1;
			}
		}
		base.Projectile.velocity.Y += 1.1f;
		if (base.Projectile.velocity.Y > 24f)
		{
			base.Projectile.velocity.Y = 24f;
		}
	}

	public void PerformGroundCollisionEffects()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		SquishFactor = 1.4f;
		int dustID = 182;
		int dustCount = 54;
		for (int i = 0; i < dustCount; i += 2)
		{
			float pairSpeed = Main.rand.NextFloat(0.5f, 16f);
			Dust dust = Dust.NewDustDirect(base.Projectile.Bottom, 0, 0, dustID);
			dust.velocity = Vector2.UnitX * pairSpeed;
			dust.scale = 2.7f;
			dust.noGravity = true;
			Dust dust2 = Dust.NewDustDirect(base.Projectile.BottomRight, 0, 0, dustID);
			dust2.velocity = Vector2.UnitX * (0f - pairSpeed);
			dust2.scale = 2.7f;
			dust2.noGravity = true;
		}
		SoundEngine.PlaySound(in SoundID.DD2_ExplosiveTrapExplode, base.Projectile.Center);
		Owner.SetScreenshake(Utils.Remap(Owner.Distance(base.Projectile.Center), 1800f, 1000f, 0f, 4.5f));
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsDropPodGlow", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 scale = base.Projectile.scale * new Vector2(SquishFactor, 1f / SquishFactor);
		Vector2 origin = frame.Size() * new Vector2(0.5f, 0.5f / SquishFactor);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(glowmask, drawPosition, frame, Color.White, base.Projectile.rotation, origin, scale, (SpriteEffects)0);
		return false;
	}
}
