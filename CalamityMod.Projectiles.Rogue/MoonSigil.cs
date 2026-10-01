using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class MoonSigil : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 250;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		if (base.Projectile.timeLeft < 52)
		{
			base.Projectile.alpha += 5;
			base.Projectile.scale -= 0.013f;
		}
		if (base.Projectile.alpha >= 255)
		{
			base.Projectile.alpha = 255;
			base.Projectile.Kill();
		}
		else
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 300f, 8f, 20f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		int cap = 5;
		float capDamageFactor = 0.05f;
		int excessCount = Main.player[base.Projectile.owner].ownedProjectileCounts[base.Type] - cap;
		modifiers.SourceDamage *= MathHelper.Clamp(1f - capDamageFactor * (float)excessCount, 0f, 1f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		float dustSp = 0.2f;
		int dustD = 0;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				Vector2 dustspeed = Utils.RotatedBy(new Vector2(dustSp, dustSp), (double)MathHelper.ToRadians((float)dustD), default(Vector2));
				int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 31, dustspeed.X, dustspeed.Y, 200, new Color(213, 242, 232, 200));
				Main.dust[d].noGravity = true;
				Main.dust[d].position = base.Projectile.Center;
				Main.dust[d].velocity = dustspeed;
				dustSp += 0.2f;
			}
			dustD += 90;
			dustSp = 0.2f;
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/MoonSigil", (AssetRequestMode)2).Value;
		Main.spriteBatch.Draw(texture, new Vector2(base.Projectile.position.X - Main.screenPosition.X + (float)base.Projectile.width * 0.5f, base.Projectile.position.Y - Main.screenPosition.Y + (float)base.Projectile.height - 10f), (Rectangle?)new Rectangle(0, 0, 20, 20), Color.White, base.Projectile.rotation, new Vector2(10f, 10f), base.Projectile.scale, (SpriteEffects)0, 0f);
	}
}
