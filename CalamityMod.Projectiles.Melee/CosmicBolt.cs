using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.extraUpdates = 100;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 spawnPos = base.Projectile.position;
				spawnPos -= base.Projectile.velocity * ((float)i * 0.25f);
				base.Projectile.alpha = 255;
				int d = Dust.NewDust(spawnPos, 1, 1, 242, 0f, 0f, 0, default(Color), 1.3f);
				Dust obj = Main.dust[d];
				obj.position = spawnPos;
				obj.position.X += base.Projectile.width / 2;
				obj.position.Y += base.Projectile.height / 2;
				obj.scale = Main.rand.NextFloat(0.49f, 0.763f);
				obj.velocity *= 0.2f;
				obj.noGravity = true;
			}
		}
	}
}
