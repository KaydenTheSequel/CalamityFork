using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Shortswords;

public class SaharaSlicersBlade : BaseShortswordProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SaharaSlicers>();

	public override string Texture => "CalamityMod/Projectiles/Melee/SaharaSlicersBlade";

	public ref int Bolts => ref Main.player[base.Projectile.owner].Calamity().saharaSlicersBolts;

	public override float FadeInDuration => 8f;

	public override float FadeOutDuration => 0f;

	public override float TotalDuration => 12f;

	public override void SetDefaults()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Size = new Vector2(15f);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.scale = 1f;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.timeLeft = 360;
		base.Projectile.hide = true;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void SetVisualOffsets()
	{
		int HalfProjWidth = base.Projectile.width / 2;
		int HalfProjHeight = base.Projectile.height / 2;
		base.DrawOriginOffsetX = 0f;
		base.DrawOffsetX = -(16 - HalfProjWidth);
		base.DrawOriginOffsetY = -(16 - HalfProjHeight);
	}

	public override void ExtraBehavior()
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(2))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(12f, 12f), 288);
			dust.scale = Main.rand.NextFloat(0.15f, 0.6f);
			dust.noGravity = true;
			dust.velocity = -base.Projectile.velocity * 0.5f;
		}
		float armPointingDirection = (base.Owner.Calamity().mouseWorld - base.Owner.MountedCenter).ToRotation();
		base.Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
		base.Owner.heldProj = base.Projectile.whoAmI;
		base.Projectile.Center = base.Projectile.Center + new Vector2(0f, 1.5f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			if (Bolts < 10)
			{
				Bolts++;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Owner.Center, Vector2.Zero, ModContent.ProjectileType<SaharaSlicersBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, Bolts).tileCollide = false;
			}
		}
		float numberOfDusts = 5f;
		float rotFactor = 360f / numberOfDusts;
		for (int j = 0; (float)j < numberOfDusts; j++)
		{
			float rot = MathHelper.ToRadians((float)j * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(0.5f, 2.5f), 0f), (double)(rot * Main.rand.NextFloat(1.1f, 9.1f)), default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(0.5f, 2.5f), 0f), (double)(rot * Main.rand.NextFloat(1.1f, 9.1f)), default(Vector2));
			Dust dust = Dust.NewDustPerfect(target.Center + offset, Main.rand.NextBool() ? 288 : 207, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
			dust.noGravity = false;
			dust.velocity = velOffset;
			dust.scale = Main.rand.NextFloat(1.5f, 1.2f);
		}
	}
}
