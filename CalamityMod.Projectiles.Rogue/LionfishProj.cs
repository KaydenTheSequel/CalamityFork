using System;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class LionfishProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Lionfish";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.timeLeft = CalamityUtils.SecondsToFrames(20f);
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 45f)
			{
				float horizontalMult = 0.98f;
				float fallSpeed = 0.35f;
				base.Projectile.velocity.X *= horizontalMult;
				base.Projectile.velocity.Y += fallSpeed;
			}
			if (base.Projectile.Calamity().stealthStrike && base.Projectile.timeLeft % 7 == 0 && base.Projectile.owner == Main.myPlayer)
			{
				Vector2 velocity = base.Projectile.DirectionFrom(Main.player[base.Projectile.owner].Center);
				velocity *= Main.rand.NextFloat(4.5f, 6.5f);
				velocity = velocity.RotatedBy((Main.rand.NextDouble() - 0.5) * Math.PI * 0.5);
				int spike = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<UrchinSpikeFugu>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
				if (spike.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[spike].DamageType = RogueDamageClass.Instance;
				}
			}
		}
		base.Projectile.StickyProjAI(15);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.ModifyHitNPCSticky(3);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[0] != 1f)
		{
			return base.CanDamage();
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, spriteEffects);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		base.Projectile.ExpandHitboxBy(72);
		for (int d = 0; d < 3; d++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
		}
		for (int i = 0; i < 30; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 2f;
			Main.dust[idx].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.type == 50 || target.type == 113 || target.type == 114 || target.type == 35 || target.type == 36)
		{
			target.buffImmune[70] = false;
		}
		target.AddBuff(70, 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(70, 240);
	}
}
