using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SlickCaneProjectile : BaseSpearProjectile
{
	private bool initialized;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SlickCane>();

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SlickCane";

	public override SpearType SpearAiType => SpearType.GhastlyGlaiveSpear;

	public override float TravelSpeed => 8f;

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 36;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.timeLeft = 120;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.hide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.alpha = 180;
		base.Projectile.scale = 1.25f;
	}

	public override bool PreAI()
	{
		if (!initialized)
		{
			Main.player[base.Projectile.owner].Calamity().ConsumeStealthByAttacking();
			initialized = true;
		}
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Texture2D alternateHookTexture = ((base.Projectile.spriteDirection == -1) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/SlickCaneProjectileAlt", (AssetRequestMode)2).Value : TextureAssets.Projectile[base.Type].Value);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((base.Projectile.spriteDirection == 1) ? ((float)alternateHookTexture.Width + 8f) : (-8f), -8f);
		Main.EntitySpriteDraw(alternateHookTexture, drawPosition, null, lightColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		float f2 = base.Projectile.rotation - (float)Math.PI / 4f * (float)Math.Sign(base.Projectile.velocity.X) + (float)(base.Projectile.spriteDirection == -1).ToInt() * (float)Math.PI;
		float velocityMagnitude = 0f;
		float scaleFactor = 18f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Main.player[base.Projectile.owner].Center, base.Projectile.Center + f2.ToRotationVector2() * scaleFactor, (TravelSpeed + 1f) * base.Projectile.scale, ref velocityMagnitude))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		if (base.Projectile.owner != Main.myPlayer || !target.IsAnEnemy(allowStatues: false) || target.dontCountMe || CalamityNPCSets.ForceDrawDebuffDisplay[target.type])
		{
			return;
		}
		float moneyValueToDrop = target.value / Main.rand.NextFloat(15f, 35f);
		moneyValueToDrop = (int)MathHelper.Clamp(moneyValueToDrop, 0f, 5000f);
		if (base.Projectile.Calamity().stealthStrike && Main.rand.NextBool(20))
		{
			moneyValueToDrop += (float)Item.buyPrice(0, Main.rand.Next(1, 3), Main.rand.Next(0, 100), Main.rand.Next(0, 100));
			SoundEngine.PlaySound(in TheSevensStriker.TriplesSound, base.Projectile.Center);
		}
		else if (moneyValueToDrop > 0f)
		{
			SoundEngine.PlaySound(in TheSevensStriker.DoublesSound, base.Projectile.Center);
		}
		while (moneyValueToDrop > 10000f)
		{
			int modifiedMoneyValue = (int)(moneyValueToDrop / 10000f);
			if (modifiedMoneyValue > 50 && Main.rand.NextBool(5))
			{
				modifiedMoneyValue /= Main.rand.Next(3) + 1;
			}
			if (Main.rand.NextBool(5))
			{
				modifiedMoneyValue /= Main.rand.Next(3) + 1;
			}
			moneyValueToDrop -= (float)(10000 * modifiedMoneyValue);
			Item.NewItem(target.GetSource_Loot(), target.Hitbox, 73, modifiedMoneyValue);
		}
		while (moneyValueToDrop > 100f)
		{
			int modifiedMoneyValue2 = (int)(moneyValueToDrop / 100f);
			if (modifiedMoneyValue2 > 50 && Main.rand.NextBool(5))
			{
				modifiedMoneyValue2 /= Main.rand.Next(3) + 1;
			}
			if (Main.rand.NextBool(5))
			{
				modifiedMoneyValue2 /= Main.rand.Next(3) + 1;
			}
			moneyValueToDrop -= (float)(100 * modifiedMoneyValue2);
			Item.NewItem(target.GetSource_Loot(), target.Hitbox, 72, modifiedMoneyValue2);
		}
		while (moneyValueToDrop > 0f)
		{
			int modifiedMoneyValue3 = (int)moneyValueToDrop;
			if (modifiedMoneyValue3 > 50 && Main.rand.NextBool(5))
			{
				modifiedMoneyValue3 /= Main.rand.Next(3) + 1;
			}
			if (Main.rand.NextBool(5))
			{
				modifiedMoneyValue3 /= Main.rand.Next(4) + 1;
			}
			if (modifiedMoneyValue3 < 1)
			{
				modifiedMoneyValue3 = 1;
			}
			moneyValueToDrop -= (float)modifiedMoneyValue3;
			Item.NewItem(target.GetSource_Loot(), target.Hitbox, 71, modifiedMoneyValue3);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		Player player = Main.player[base.Projectile.owner];
		double money = Utils.CoinsCount(out var _, player.inventory);
		double cap = 1000000.0;
		if (money >= cap)
		{
			money = cap;
		}
		if (money != 0.0)
		{
			modifiers.SourceDamage *= (float)(money / 1000000.0 + 1.0);
			SoundEngine.PlaySound(in TheSevensStriker.JackpotSound, base.Projectile.Center);
			for (int j = 0; j < 8; j++)
			{
				int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dust2].noGravity = true;
				Dust obj = Main.dust[dust2];
				obj.velocity *= 5f;
				dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, default(Color), 2f);
				Dust obj2 = Main.dust[dust2];
				obj2.velocity *= 2f;
			}
		}
	}
}
