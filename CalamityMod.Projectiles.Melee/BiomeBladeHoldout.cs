using System;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BiomeBladeHoldout : ModProjectile, ILocalizedModType, IModType
{
	private Item associatedItem;

	private const int ChannelTime = 120;

	public bool drawIndrawHeldProjInFrontOfHeldItemAndArms = true;

	public CalamityUtils.CurveSegment anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineOut, 0f, 1f, 0.35f);

	public CalamityUtils.CurveSegment thrust = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpIn, 0.85f, 1.35f, -1.45f);

	public CalamityUtils.CurveSegment bounceback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineOut, 0.95f, -0.1f, 0.1f);

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
			return (Owner.HeldItem.ModItem as TrueBiomeBlade).CanUseItem(Owner);
		}
	}

	public bool OwnerMayChannel
	{
		get
		{
			if (Owner.itemAnimation == 0 && OwnerCanUseItem && Owner.Calamity().mouseRight && Owner.active && !Owner.dead && Owner.StandingStill() && !Owner.mount.Active)
			{
				return Owner.CheckSolidGround(1, 3);
			}
			return false;
		}
	}

	public ref float ChanneledState => ref base.Projectile.ai[0];

	public ref float ChannelTimer => ref base.Projectile.ai[1];

	public ref float Initialized => ref base.Projectile.localAI[0];

	public override string Texture => "CalamityMod/Items/Weapons/Melee/TrueBiomeBlade";

	public override void SetStaticDefaults()
	{
	}

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
		return CalamityUtils.PiecewiseAnimation(ChannelTimer / 120f, anticipation, thrust, bounceback);
	}

	public override void AI()
	{
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		if (Initialized == 0f)
		{
			if (Owner.HeldItem.type != ModContent.ItemType<TrueBiomeBlade>())
			{
				base.Projectile.Kill();
				return;
			}
			if (Owner.whoAmI == Main.myPlayer)
			{
				SoundEngine.PlaySound(in SoundID.DD2_DarkMageHealImpact);
			}
			associatedItem = Owner.HeldItem;
			Attunement temporaryAttunementStorage = (associatedItem.ModItem as TrueBiomeBlade).mainAttunement;
			(associatedItem.ModItem as TrueBiomeBlade).mainAttunement = (associatedItem.ModItem as TrueBiomeBlade).secondaryAttunement;
			(associatedItem.ModItem as TrueBiomeBlade).secondaryAttunement = temporaryAttunementStorage;
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
			base.Projectile.Center = Owner.Center + new Vector2((16f * (float)Owner.direction + (float)Owner.direction < 0f) ? (-26f) : 16f, -30f * SwordHeight() + 10f);
			base.Projectile.rotation = (float)Math.PI * 3f / 4f;
			ChannelTimer++;
			base.Projectile.timeLeft = 60;
			if (ChannelTimer >= 120f)
			{
				Attune((TrueBiomeBlade)associatedItem.ModItem);
				base.Projectile.timeLeft = 120;
				ChanneledState = 2f;
				Color particleColor = (associatedItem.ModItem as TrueBiomeBlade).mainAttunement.tooltipColor;
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
		}
		if (ChanneledState == 1f)
		{
			Projectile projectile = base.Projectile;
			projectile.position += Vector2.UnitY * -0.3f * (1f + (float)base.Projectile.timeLeft / 60f);
		}
	}

	public void Attune(TrueBiomeBlade item)
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		bool zoneJungle = Owner.ZoneJungle;
		bool num = Owner.ZoneSnow || Owner.ZoneSkyHeight;
		bool evilAttune = Owner.ZoneCorrupt || Owner.ZoneCrimson;
		bool num2 = Owner.ZoneDesert || Owner.ZoneUnderworldHeight;
		bool heavenlyAttune = Owner.ZoneHallow || Owner.Calamity().ZoneAstral;
		Attunement attunement = AttunementSystem.FindOrNull(AttunementID.TrueDefault);
		if (num2)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.TrueHot);
		}
		if (num)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.TrueCold);
		}
		if (zoneJungle)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.TrueTropical);
		}
		if (evilAttune)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.TrueEvil);
		}
		if (heavenlyAttune)
		{
			attunement = AttunementSystem.FindOrNull(AttunementID.Holy);
		}
		if (item.secondaryAttunement == attunement)
		{
			SoundEngine.PlaySound(in SoundID.DD2_LightningBugZap, base.Projectile.Center);
			item.secondaryAttunement = item.mainAttunement;
			item.mainAttunement = attunement;
		}
		else
		{
			SoundEngine.PlaySound(in SoundID.DD2_MonkStaffGroundImpact, base.Projectile.Center);
			item.mainAttunement = attunement;
		}
	}

	public override void OnKill(int timeLeft)
	{
		if (associatedItem != null && (associatedItem.ModItem as TrueBiomeBlade).mainAttunement == null && (associatedItem.ModItem as TrueBiomeBlade).secondaryAttunement != null)
		{
			(associatedItem.ModItem as TrueBiomeBlade).mainAttunement = (associatedItem.ModItem as TrueBiomeBlade).secondaryAttunement;
			(associatedItem.ModItem as TrueBiomeBlade).secondaryAttunement = null;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		if (ChanneledState == 0f && ChannelTimer > 6f)
		{
			return base.PreDraw(ref lightColor);
		}
		if (ChanneledState == 1f)
		{
			Texture2D tex = TextureAssets.Projectile[base.Type].Value;
			Vector2 squishyScale = default(Vector2);
			((Vector2)(ref squishyScale))._002Ector(Math.Abs((float)Math.Sin((float)Math.PI + (float)Math.PI * 2f * (float)base.Projectile.timeLeft / 30f)), 1f);
			SpriteEffects flip = (SpriteEffects)(!((float)Math.Sin((float)Math.PI + (float)Math.PI * 2f * (float)base.Projectile.timeLeft / 30f) > 0f));
			Main.EntitySpriteDraw(tex, base.Projectile.position - Main.screenPosition, null, lightColor * ((float)base.Projectile.timeLeft / 60f), 0f, tex.Size() / 2f, squishyScale * (2f - (float)base.Projectile.timeLeft / 60f), flip);
			return false;
		}
		return false;
	}
}
