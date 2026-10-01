using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EnchantedKnifeStaffProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.netUpdate = true;
	}

	public override void AI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ && base.Projectile.timeLeft % 2 == 0)
		{
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f);
			int type = ((!Main.rand.NextBool()) ? 15 : (Main.rand.NextBool() ? 57 : 58));
			Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f);
			Color newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
			Vector2 center = base.Projectile.Center;
			newColor = Color.Cyan;
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.2f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.timeLeft = 3;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color cyan = Color.Cyan;
		((Color)(ref cyan)).A = 0;
		Color drawColor = cyan * 1.2f;
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 2f;
		Vector2 anchorPoint = value.Size() * 0.5f;
		Main.EntitySpriteDraw(value, drawPosition, null, drawColor, drawRotation, anchorPoint, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
