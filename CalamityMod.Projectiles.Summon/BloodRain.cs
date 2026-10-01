using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BloodRain : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.aiStyle = 45;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 2;
		base.Projectile.scale = 1.1f;
		base.AIType = 239;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		int blood = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100);
		Dust obj = Main.dust[blood];
		obj.velocity = Vector2.Zero;
		obj.position -= base.Projectile.velocity / 5f;
		obj.noGravity = true;
		obj.scale = 2f;
		obj.noLight = true;
	}
}
