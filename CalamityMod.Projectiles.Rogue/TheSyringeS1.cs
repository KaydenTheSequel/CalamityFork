using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TheSyringeS1 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.aiStyle = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 50;
		base.Projectile.scale = 1.2f;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 150 && target.CanBeChasedBy(base.Projectile);
	}

	public override void PostAI()
	{
		if (base.Projectile.timeLeft < 150)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 10f, 20f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/TheSyringeS2", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture.Width, 6), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, 10f), base.Projectile.scale, (SpriteEffects)0, 0f);
			return false;
		}
		if (base.Projectile.ai[0] == 2f)
		{
			Texture2D texture2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/TheSyringeS3", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(texture2, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture2.Width, 6), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2.Width / 2f, 10f), base.Projectile.scale, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}
}
