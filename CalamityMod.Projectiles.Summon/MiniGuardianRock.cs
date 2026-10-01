using System;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.NPCs.ProfanedGuardians;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MiniGuardianRock : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/NPCs/ProfanedGuardians/ProfanedRocks";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.minion = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 1;
	}

	public override void AI()
	{
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		if (Owner.Calamity().pSoulGuardians && base.Projectile.ai[0] == 0f)
		{
			base.Projectile.timeLeft = 4;
		}
		if (!Owner.Calamity().pSoulArtifact || Owner.dead || !Owner.active || (Owner.Calamity().profanedCrystal && !Owner.Calamity().profanedCrystalBuffs))
		{
			Owner.Calamity().pSoulGuardians = false;
			base.Projectile.active = false;
		}
		else if (base.Projectile.ai[0] == 0f)
		{
			_ = 0.2f;
			_ = base.Projectile.ai[1];
			int psc = Owner.Calamity().pscState;
			_ = (float)Math.PI * 2f / (float)((psc > 0) ? 10 : 5);
			_ = base.Projectile.ai[1];
			float distance = 50f + ((psc > 0) ? 30f : 0f);
			base.Projectile.Center = Owner.Center + base.Projectile.ai[1].ToRotationVector2() * distance;
			base.Projectile.rotation = base.Projectile.ai[1] + (float)Math.Atan(90.0);
			base.Projectile.ai[1] += MathHelper.ToRadians((psc > 0) ? 2f : (-2f));
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			NPC target = base.Projectile.Center.MinionHoming(2000f, Owner);
			if (Owner.HasBuff<ProfanedCrystalWhipBuff>() && target != null)
			{
				base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, target, 32f);
				base.Projectile.ai[0] = 3f;
			}
			else
			{
				base.Projectile.velocity = base.Projectile.Center - Owner.Center;
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile = base.Projectile;
				projectile.velocity *= (Owner.Calamity().profanedCrystalBuffs ? 25f : 20f);
				base.Projectile.ai[0] = 2f;
			}
			base.Projectile.timeLeft = 300;
		}
		else
		{
			if (base.Projectile.ai[0] != 2f)
			{
				return;
			}
			if (base.Projectile.timeLeft > 275)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.9725f;
			}
			for (int i = 0; i < 2; i++)
			{
				if (i == 0 || base.Projectile.timeLeft > 285)
				{
					Dust.NewDust(base.Projectile.position, base.Projectile.width / 2, base.Projectile.height / 2, 244, 0f, -1f);
				}
			}
		}
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.ai[0] >= 1f))
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		int dye = Owner?.cMinion ?? 0;
		bool psc = Owner.Calamity().profanedCrystalBuffs;
		int rockType = (int)MathHelper.Clamp(base.Projectile.ai[2], 1f, 6f);
		Texture2D texture = ProfanedRocks.Textures[rockType - 1].Value;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)(texture.Width / 2), (float)(texture.Height / 2));
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		drawPos -= new Vector2((float)texture.Width, (float)texture.Height) * base.Projectile.scale / 2f;
		drawPos += drawOrigin * base.Projectile.scale + new Vector2(0f, base.Projectile.gfxOffY);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 0, texture.Width, texture.Height);
		float ownerDist = base.Projectile.Center.Distance(Owner.Center);
		float lerpVal = Utils.GetLerpValue(psc ? 72 : 42, psc ? 87 : 57, ownerDist, clamped: true);
		float mult = MathHelper.Lerp(0.35f, 0.42f, lerpVal);
		if ((psc && ownerDist > 87f) || (!psc && ownerDist > 57f))
		{
			lerpVal = Utils.GetLerpValue(psc ? 87 : 57, psc ? 150 : 120, ownerDist, clamped: true);
			mult = MathHelper.Lerp(0.42f, 1f, lerpVal);
		}
		if (CalamityClientConfig.Instance.Afterimages && base.Projectile.ai[0] >= 1f)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				drawPos = base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
				Color color = base.Projectile.GetAlpha(lightColor) * ((float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length);
				color *= mult;
				DrawData drawData = new DrawData(texture, drawPos, frame, color);
				drawData.rotation = base.Projectile.rotation;
				drawData.origin = drawOrigin;
				DrawData drawData2 = drawData;
				GameShaders.Armor.Apply(dye, base.Projectile, drawData2);
				Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)frame, color, base.Projectile.rotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0, 0f);
			}
		}
		else
		{
			Color color2 = Color.White * mult;
			DrawData drawData = new DrawData(texture, drawPos, frame, color2);
			drawData.rotation = base.Projectile.rotation;
			drawData.origin = drawOrigin;
			DrawData drawData3 = drawData;
			GameShaders.Armor.Apply(dye, base.Projectile, drawData3);
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)frame, color2, base.Projectile.rotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 10; k++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, -1f);
		}
	}
}
