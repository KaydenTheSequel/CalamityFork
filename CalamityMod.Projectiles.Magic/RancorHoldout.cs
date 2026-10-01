using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RancorHoldout : ModProjectile
{
	public const int ManaConsumeRate = 12;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Rancor>();

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 16;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 34);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 90000;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.Center + Vector2.UnitX * (float)Owner.direction * 8f;
		bool allowContinuedUse = Time % 12f != 11f || Owner.CheckMana(Owner.HeldItem, -1, pay: true);
		if (!(Owner.channel & allowContinuedUse) || Owner.noItems || Owner.CCed)
		{
			base.Projectile.Kill();
			return;
		}
		Time++;
		if (Main.myPlayer == base.Projectile.owner && Time == 1f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<RancorMagicCircle>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 4)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
		AdjustPlayerHoldValues();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public void AdjustPlayerHoldValues()
	{
		base.Projectile.spriteDirection = (base.Projectile.direction = Owner.direction);
		base.Projectile.timeLeft = 2;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = 0f;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
