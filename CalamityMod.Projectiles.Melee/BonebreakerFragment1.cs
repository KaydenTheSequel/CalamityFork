using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BonebreakerFragment1 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.aiStyle = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.alpha = 50;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BonebreakerFragment2", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture.Width, texture.Height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
			return false;
		}
		if (base.Projectile.ai[0] == 2f)
		{
			Texture2D texture2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BonebreakerFragment2", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(texture2, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture2.Width, texture2.Height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2.Width / 2f, (float)texture2.Height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.DamageType == DamageClass.Melee)
		{
			target.AddBuff(70, 60);
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 60);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.DamageType == DamageClass.Melee)
		{
			target.AddBuff(70, 60);
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 60);
		}
	}
}
