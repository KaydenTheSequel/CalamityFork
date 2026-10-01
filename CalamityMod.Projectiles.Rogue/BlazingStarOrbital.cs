using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BlazingStarOrbital : ModProjectile, ILocalizedModType, IModType
{
	private static int Lifetime = 300;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/BlazingStar";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 272;
		base.Projectile.height = 272;
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
		Lifetime = 300;
		base.Projectile.ai[0]++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		float distance = MathHelper.Min(base.Projectile.ai[0] / 15f, 1f);
		if (base.Projectile.timeLeft < 15)
		{
			distance = (float)base.Projectile.timeLeft / 15f;
		}
		int firstRingGlaives = 3;
		int secondRingGlaives = 6;
		for (int i2 = 0; i2 < 5; i2++)
		{
			for (int j = 0; j < firstRingGlaives; j++)
			{
				float glaiveRot = (float)Math.PI * 2f * ((float)j / (float)firstRingGlaives);
				float opacity = (float)i2 / 5f;
				Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center + Utils.RotatedBy(new Vector2(distance * 64f, 0f), (double)(base.Projectile.rotation + glaiveRot + 1f * (float)i2 / 5f), default(Vector2)) - Main.screenPosition, (Rectangle?)null, lightColor * opacity, Main.GlobalTimeWrappedHourly * -10f, TextureAssets.Projectile[base.Type].Size() * 0.5f, 0.5f * opacity + 0.5f, (SpriteEffects)0, 1f);
			}
			for (int k = 0; k < secondRingGlaives; k++)
			{
				float glaiveRot2 = (float)Math.PI * 2f * ((float)k / (float)secondRingGlaives);
				float opacity2 = 1f - (float)i2 / 5f;
				Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center + Utils.RotatedBy(new Vector2(distance * 128f, 0f), (double)((0f - base.Projectile.rotation) * 0.5f + glaiveRot2 + 0.45f * (float)i2 / 5f), default(Vector2)) - Main.screenPosition, (Rectangle?)null, lightColor * opacity2, Main.GlobalTimeWrappedHourly * 10f, TextureAssets.Projectile[base.Type].Size() * 0.5f, 0.5f * opacity2 + 0.5f, (SpriteEffects)0, 1f);
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
