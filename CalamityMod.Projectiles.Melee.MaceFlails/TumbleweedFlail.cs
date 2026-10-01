using System;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.MaceFlails;

public class TumbleweedFlail : BaseMaceFlailProjectile
{
	public static float MaxAuraTime = 60f;

	private bool hasDetached;

	private int detachDelay;

	public override int AssociatedItemID => ModContent.ItemType<Tumbleweed>();

	public override int SpinIFrames => 10;

	public override float SpinHitboxRadius => MathF.Max(64f, 224f * AuraScale);

	public override float SpinVerticalFactor => 1f;

	public override float SpinVisualRadius => 45f;

	public override float LaunchSpeed => 24f;

	public override int LaunchLifespan => 20;

	public override float MaxDropRange => 640f;

	public override float MaxRetractSpeed => 28f;

	public override float RetractAcceleration => 4f;

	public ref float AuraScale => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 42);
		base.SetDefaults();
	}

	public override bool ExtraBehavior()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		if ((base.CurrentFlailState == FlailState.Spinning || ((Vector2)(ref base.Projectile.velocity)).Length() > 2f) && base.Projectile.soundDelay <= 0)
		{
			SoundStyle style = SoundID.Grass with
			{
				PitchVariance = 1.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.soundDelay = Main.rand.Next(12, 15);
		}
		if (base.CurrentFlailState != FlailState.Spinning || Main.rand.NextBool())
		{
			Vector2 position = base.Projectile.position;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			float scale = Main.rand.NextFloat(0.6f, 1.2f);
			Dust dust = Dust.NewDustDirect(position, width, height, 32, 0f, 0f, 100, default(Color), scale);
			dust.noGravity = base.CurrentFlailState == FlailState.Dropping;
			dust.velocity = ((base.CurrentFlailState == FlailState.Spinning) ? (Main.rand.NextVector2Unit() * 1.5f) : (base.Projectile.velocity * 0.25f));
		}
		if (detachDelay > 0)
		{
			detachDelay--;
			base.CurrentFlailState = ((detachDelay != 0) ? FlailState.LaunchingForward : FlailState.ForcedRetracting);
		}
		if (base.CurrentFlailState == FlailState.Spinning)
		{
			base.Projectile.ownerHitCheck = false;
			AuraScale = MathHelper.Clamp(AuraScale + 1f / MaxAuraTime, 0f, 1f);
			if (AuraScale < 0.15f)
			{
				return true;
			}
			for (int i = 0; i < (int)(10f * AuraScale); i++)
			{
				Circle dustCircle = new Circle(base.Owner.MountedCenter, 240f * AuraScale);
				Vector2 dustPos = dustCircle.RandomPointInCircle();
				Vector2 center = dustPos - base.Owner.MountedCenter;
				if (((Vector2)(ref center)).Length() > 60f * AuraScale)
				{
					Dust dust2 = Dust.NewDustPerfect(dustPos, 32);
					dust2.noGravity = true;
					dust2.fadeIn = Main.rand.NextFloat(0.4f, 1f);
					Vector2 spinningpoint = (dustCircle.Center - dustPos).SafeNormalize(Vector2.Zero);
					center = default(Vector2);
					dust2.velocity = spinningpoint.RotatedBy(-0.7853981852531433, center) * Vector2.Distance(dustCircle.Center, dustPos) * 0.04f;
				}
			}
		}
		else
		{
			AuraScale = MathHelper.Clamp(AuraScale - 3f / MaxAuraTime, 0f, 1f);
		}
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (AuraScale > 0f)
		{
			Texture2D Aura = TextureAssets.Projectile[ModContent.ProjectileType<SandCloakVeil>()].Value;
			Vector2 position = base.Owner.MountedCenter - Main.screenPosition;
			Color drawColor = base.Projectile.GetAlpha(lightColor) * 0.05f * AuraScale;
			for (int i = 0; i < 20; i++)
			{
				Main.EntitySpriteDraw(Aura, position, null, drawColor, Main.GlobalTimeWrappedHourly * 0.5f + (float)(i * i) * 0.03f, Aura.Size() * 0.5f, AuraScale - (float)i * 0.03f, (SpriteEffects)0);
			}
		}
		if (hasDetached)
		{
			DrawChain();
			return false;
		}
		return base.PreDraw(ref lightColor);
	}

	public override bool? CanDamage()
	{
		if (hasDetached)
		{
			return false;
		}
		return base.CanDamage();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		if (base.CurrentFlailState == FlailState.Spinning)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.NPCDeath15, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			Dust tumbleDust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 32, 0f, 0f, 100, default(Color), 1.2f);
			Dust dust = tumbleDust;
			dust.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				tumbleDust.scale = 0.5f;
				tumbleDust.fadeIn = Main.rand.NextFloat(1f, 1.1f);
			}
			tumbleDust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 85, 0f, 0f, 100, default(Color), 1.7f);
			tumbleDust.noGravity = true;
			Dust dust2 = tumbleDust;
			dust2.velocity *= 5f;
			tumbleDust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 85, 0f, 0f, 100);
			Dust dust3 = tumbleDust;
			dust3.velocity *= 2f;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<TumbleweedRolling>(), (int)((float)base.Projectile.damage * LaunchDamage), base.Projectile.knockBack, base.Projectile.owner, (base.CurrentFlailState == FlailState.LaunchingForward) ? 0f : 1f);
		}
		hasDetached = true;
		if ((base.CurrentFlailState == FlailState.LaunchingForward || base.CurrentFlailState == FlailState.Ricochet) && base.StateTimer < 6f)
		{
			detachDelay = 6;
		}
		else
		{
			base.CurrentFlailState = FlailState.ForcedRetracting;
		}
	}
}
