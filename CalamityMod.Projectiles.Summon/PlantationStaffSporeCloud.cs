using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlantationStaffSporeCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float RandomTexture => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.idStaticNPCHitCooldown = 30;
		base.Projectile.timeLeft = 600;
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.985f;
		base.Projectile.rotation += MathHelper.ToRadians(base.Projectile.velocity.X);
		base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 180f, 0f, 0f, 255f);
		if (Main.rand.NextBool(10))
		{
			GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(base.Projectile.Center + Main.rand.NextVector2Circular(base.Projectile.width / 2, base.Projectile.height / 2), Main.rand.NextVector2Circular(1f, 1f), Color.Green, Color.DarkGreen, Utils.Remap(base.Projectile.timeLeft, 180f, 0f, 1.2f, 0.2f), Utils.Remap(base.Projectile.timeLeft, 180f, 0f, 150f, 0f)));
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 46);
			dust.noGravity = true;
			dust.velocity = Vector2.Zero;
			dust.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 180f, 0f, 0f, 255f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		if (RandomTexture == 1f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/PlantationStaffSporeCloud2", (AssetRequestMode)2).Value;
		}
		if (RandomTexture == 2f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/PlantationStaffSporeCloud3", (AssetRequestMode)2).Value;
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
