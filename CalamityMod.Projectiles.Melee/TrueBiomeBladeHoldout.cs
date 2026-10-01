using System;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TrueBiomeBladeHoldout : ModProjectile, ILocalizedModType, IModType
{
	private Item associatedItem;

	private const int ChannelTime = 120;

	public bool drawIndrawHeldProjInFrontOfHeldItemAndArms = true;

	public CalamityUtils.CurveSegment anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0f, -0.3f);

	public CalamityUtils.CurveSegment rise = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpIn, 0f, 0f, 1f);

	public CalamityUtils.CurveSegment overshoot = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.8f, 1f, 0.1f);

	public new string LocalizationCategory => "Projectiles.Melee";

	private Player Owner => Main.player[base.Projectile.owner];

	public bool OwnerCanUseItem
	{
		get
		{
			if (Owner.HeldItem != associatedItem)
			{
				return false;
			}
			return (Owner.HeldItem.ModItem as OmegaBiomeBlade).CanUseItem(Owner);
		}
	}

	public bool OwnerMayChannel
	{
		get
		{
			if (Owner.itemAnimation == 0 && OwnerCanUseItem && Owner.Calamity().mouseRight && Owner.active)
			{
				return !Owner.dead;
			}
			return false;
		}
	}

	public ref float ChanneledState => ref base.Projectile.ai[0];

	public ref float ChannelTimer => ref base.Projectile.ai[1];

	public ref float Initialized => ref base.Projectile.localAI[0];

	public override string Texture => "CalamityMod/Items/Weapons/Melee/OmegaBiomeBlade";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.aiStyle = -1;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.tileCollide = false;
		base.Projectile.damage = 0;
	}

	internal float SwordHeight()
	{
		return CalamityUtils.PiecewiseAnimation(ChannelTimer / 120f, rise, overshoot);
	}

	public override void AI()
	{
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		if (Initialized == 0f)
		{
			if (Owner.HeldItem.type != ModContent.ItemType<OmegaBiomeBlade>())
			{
				base.Projectile.Kill();
				return;
			}
			if (Owner.whoAmI == Main.myPlayer)
			{
				SoundEngine.PlaySound(in SoundID.DD2_DarkMageHealImpact);
			}
			associatedItem = Owner.HeldItem;
			Attunement temporaryAttunementStorage = (associatedItem.ModItem as OmegaBiomeBlade).mainAttunement;
			(associatedItem.ModItem as OmegaBiomeBlade).mainAttunement = (associatedItem.ModItem as OmegaBiomeBlade).secondaryAttunement;
			(associatedItem.ModItem as OmegaBiomeBlade).secondaryAttunement = temporaryAttunementStorage;
			Initialized = 1f;
		}
		if (!OwnerMayChannel && ChanneledState == 0f)
		{
			base.Projectile.Center = Owner.Top + new Vector2(18f, 0f);
			ChanneledState = 1f;
			base.Projectile.timeLeft = 60;
			return;
		}
		if (ChanneledState == 0f)
		{
			Owner.heldProj = base.Projectile.whoAmI;
			Owner.itemRotation = (-Vector2.UnitY).ToRotation();
			base.Projectile.Center = Owner.Top + new Vector2(0f, -20f * SwordHeight() - 50f);
			base.Projectile.rotation = -(float)Math.PI / 4f;
			ChannelTimer++;
			base.Projectile.timeLeft = 60;
			if (ChannelTimer == 105f)
			{
				Attune((OmegaBiomeBlade)associatedItem.ModItem);
				Color particleColor = (associatedItem.ModItem as OmegaBiomeBlade).mainAttunement.tooltipColor;
				for (int i = 0; i <= 5; i++)
				{
					Vector2 displace = Vector2.UnitX * 20f * Main.rand.NextFloat(-1f, 1f);
					GeneralParticleHandler.SpawnParticle(new GenericBloom(Owner.Bottom + displace, -Vector2.UnitY * Main.rand.NextFloat(1f, 5f), particleColor, 0.02f + Main.rand.NextFloat(0f, 0.2f), 20 + Main.rand.Next(30)));
				}
				for (int j = 0; j <= 10; j++)
				{
					Vector2 displace2 = Vector2.UnitX * 16f * Main.rand.NextFloat(-1f, 1f);
					GeneralParticleHandler.SpawnParticle(new GenericSparkle(Owner.Bottom + displace2, -Vector2.UnitY * Main.rand.NextFloat(1f, 5f), particleColor, particleColor, 0.5f + Main.rand.NextFloat(-0.2f, 0.2f), 20 + Main.rand.Next(30), 1f, 2f));
				}
			}
			if (ChannelTimer >= 105f)
			{
				Vector2 Shake = Main.rand.NextVector2Circular(6f, 6f);
				Projectile projectile = base.Projectile;
				projectile.Center += Shake;
				GeneralParticleHandler.SpawnParticle(new ElectricSpark(base.Projectile.Center + Vector2.UnitY * 20f, -Vector2.UnitY.RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(10f, 20f), Color.White, (!Main.rand.NextBool()) ? (Main.rand.NextBool() ? Color.Cyan : Color.Magenta) : (Main.rand.NextBool() ? Color.Goldenrod : Color.GreenYellow), 1f + Main.rand.NextFloat(0f, 1f), 34, (float)Math.PI / 4f, 10f, 0.1f, 4f));
			}
			if (ChannelTimer >= 120f)
			{
				base.Projectile.timeLeft = 60;
				ChanneledState = 2f;
			}
		}
		if (ChanneledState == 1f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.position += Vector2.UnitY * -0.3f * (1f + (float)base.Projectile.timeLeft / 60f);
		}
	}

	public void Attune(OmegaBiomeBlade item)
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		bool flailAttune = Owner.ZoneJungle || Owner.ZoneSnow || Owner.ZoneSkyHeight;
		bool num = Owner.ZoneDesert || Owner.ZoneUnderworldHeight || Owner.ZoneCorrupt || Owner.ZoneCrimson;
		bool whirlAttune = Owner.ZoneHallow || Owner.Calamity().ZoneAstral;
		Attunement attunement = AttunementSystem.FindOrNull(AttunementID.Shockwave);
		if (flailAttune)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.FlailBlade);
		}
		if (num)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.SuperPogo);
		}
		if (whirlAttune)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.Whirlwind);
		}
		if (item.secondaryAttunement == attunement)
		{
			SoundEngine.PlaySound(in SoundID.DD2_LightningBugZap, base.Projectile.Center);
			item.secondaryAttunement = item.mainAttunement;
			item.mainAttunement = attunement;
			return;
		}
		SoundStyle style = CommonCalamitySounds.LightningSound with
		{
			Volume = CommonCalamitySounds.LightningSound.Volume * 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new ThunderBoltVFX(delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + Vector2.UnitY * 20f;
		}, 0f, 1.5f, (!Main.rand.NextBool()) ? (Main.rand.NextBool() ? Color.Cyan : Color.Magenta) : (Main.rand.NextBool() ? Color.Goldenrod : Color.GreenYellow), 30, 15f));
		Main.LocalPlayer.SetScreenshake(5f);
		item.mainAttunement = attunement;
	}

	public override void OnKill(int timeLeft)
	{
		if (associatedItem != null && (associatedItem.ModItem as OmegaBiomeBlade).mainAttunement == null && (associatedItem.ModItem as OmegaBiomeBlade).secondaryAttunement != null)
		{
			(associatedItem.ModItem as OmegaBiomeBlade).mainAttunement = (associatedItem.ModItem as OmegaBiomeBlade).secondaryAttunement;
			(associatedItem.ModItem as OmegaBiomeBlade).secondaryAttunement = null;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		if (ChanneledState == 0f && ChannelTimer > 10f)
		{
			Texture2D tex = TextureAssets.Projectile[base.Type].Value;
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, tex.Size() / 2f, 1f, (SpriteEffects)0);
			return false;
		}
		if (ChanneledState == 1f)
		{
			Texture2D tex2 = TextureAssets.Projectile[base.Type].Value;
			Vector2 squishyScale = default(Vector2);
			((Vector2)(ref squishyScale))._002Ector(Math.Abs((float)Math.Sin((float)Math.PI + (float)Math.PI * 2f * (float)base.Projectile.timeLeft / 30f)), 1f);
			SpriteEffects flip = (SpriteEffects)(!((float)Math.Sin((float)Math.PI + (float)Math.PI * 2f * (float)base.Projectile.timeLeft / 30f) > 0f));
			Main.EntitySpriteDraw(tex2, base.Projectile.position - Main.screenPosition, null, lightColor * ((float)base.Projectile.timeLeft / 60f), 0f, tex2.Size() / 2f, squishyScale * (2f - (float)base.Projectile.timeLeft / 60f), flip);
			return false;
		}
		return false;
	}
}
