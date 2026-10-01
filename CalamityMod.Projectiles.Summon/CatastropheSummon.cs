using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CatastropheSummon : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Time => ref base.Projectile.ai[0];

	public bool LookingAtPlayer => Time < 45f;

	public override string Texture => "CalamityMod/NPCs/SupremeCalamitas/SupremeCatastrophe";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.MinionSacrificable[base.Type] = false;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = false;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 120);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 0f;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		if (LookingAtPlayer)
		{
			base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(0f, 6f, Time / 45f));
		}
		else
		{
			float slashInterpolant = (Time - 45f) / 27f % 1f;
			base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(6f, 15f, slashInterpolant));
		}
		CataclysmSummon.Behavior(base.Projectile, Main.player[base.Projectile.owner], ref Time);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(2, 8, base.Projectile.frame / 8, base.Projectile.frame % 8);
		Vector2 origin = frame.Size() * 0.5f;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float afterimageRot = base.Projectile.oldRot[i];
			SpriteEffects sfxForThisAfterimage = (SpriteEffects)(base.Projectile.oldSpriteDirection[i] == -1);
			Vector2 drawPos = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
			Color color = base.Projectile.GetAlpha(lightColor) * ((float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length);
			Main.EntitySpriteDraw(texture, drawPos, frame, color, afterimageRot, origin, base.Projectile.scale, sfxForThisAfterimage);
		}
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= 0f;
		if (Main.masterMode)
		{
			modifiers.SourceDamage.Flat += 320f;
		}
		else if (Main.expertMode)
		{
			modifiers.SourceDamage.Flat += 260f;
		}
		else
		{
			modifiers.SourceDamage.Flat += 200f;
		}
	}

	public override bool? CanDamage()
	{
		return base.Projectile.Opacity >= 1f;
	}
}
