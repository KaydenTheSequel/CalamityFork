using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class GlaiveOrbital : ModProjectile, ILocalizedModType, IModType
{
	private static int Lifetime = 300;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Glaive";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 136;
		base.Projectile.height = 136;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Main.player[base.Projectile.owner].Center;
		base.Projectile.rotation += 0.2f;
		base.Projectile.ai[0]++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		float distance = MathHelper.Min(MathHelper.Lerp(0f, 64f, base.Projectile.ai[0] / 15f), 64f);
		if (base.Projectile.timeLeft < 15)
		{
			distance = MathHelper.Lerp(0f, 64f, (float)base.Projectile.timeLeft / 15f);
		}
		int glaiveCount = 3;
		for (int i2 = 0; i2 < 5; i2++)
		{
			for (int j = 0; j < glaiveCount; j++)
			{
				float glaiveRot = (float)Math.PI * 2f * ((float)j / (float)glaiveCount);
				float opacity = (float)i2 / 5f;
				Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center + Utils.RotatedBy(new Vector2(distance, 0f), (double)(base.Projectile.rotation + glaiveRot + 1f * (float)i2 / 5f), default(Vector2)) - Main.screenPosition, (Rectangle?)null, lightColor * opacity, Main.GlobalTimeWrappedHourly * -10f, TextureAssets.Projectile[base.Type].Size() * 0.5f, 0.5f * opacity + 0.5f, (SpriteEffects)0, 1f);
			}
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = -target.DirectionTo(Main.player[base.Projectile.owner].Center).X.DirectionalSign();
	}
}
