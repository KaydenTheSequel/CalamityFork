using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SilvaCrystal : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 52;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		bool isMinion = base.Projectile.type == ModContent.ProjectileType<SilvaCrystal>();
		if (!modPlayer.silvaSummon)
		{
			base.Projectile.active = false;
			return;
		}
		if (isMinion)
		{
			if (player.dead)
			{
				modPlayer.sCrystal = false;
			}
			if (modPlayer.sCrystal)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY - 60f);
		if (player.gravDir == -1f)
		{
			base.Projectile.position.Y += 120f;
			base.Projectile.rotation = (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.alpha -= 5;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.direction == 0)
		{
			base.Projectile.direction = Main.player[base.Projectile.owner].direction;
		}
		if (base.Projectile.alpha == 0 && Main.rand.NextBool(15))
		{
			Dust obj = Dust.NewDustPerfect(base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(26f, 30f), 267, Vector2.UnitY * Main.rand.NextFloat(-2f, 2f), 100, new Color(Main.DiscoR, 203, 103), 0.5f);
			obj.noGravity = true;
			obj.fadeIn = 1f;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 60f)
		{
			base.Projectile.localAI[0] = 0f;
		}
		if (base.Projectile.ai[0] < 0f)
		{
			base.Projectile.ai[0]++;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			int targetID = -1;
			float attackRange = 1500f;
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(base.Projectile))
				{
					float targetDist = base.Projectile.Distance(npc.Center);
					if (targetDist < attackRange && Collision.CanHitLine(base.Projectile.Center, 0, 0, npc.Center, 0, 0))
					{
						attackRange = targetDist;
						targetID = npc.whoAmI;
					}
				}
			}
			if (targetID < 0)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC target = enumerator.Current;
					if (target.CanBeChasedBy(base.Projectile))
					{
						float targetDistance = base.Projectile.Distance(target.Center);
						if (targetDistance < attackRange && Collision.CanHitLine(base.Projectile.Center, 0, 0, target.Center, 0, 0))
						{
							attackRange = targetDistance;
							targetID = target.whoAmI;
						}
					}
				}
			}
			if (targetID != -1)
			{
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = targetID;
				base.Projectile.netUpdate = true;
				return;
			}
		}
		if (!(base.Projectile.ai[0] > 0f))
		{
			return;
		}
		int npcTrack = (int)base.Projectile.ai[1];
		if (!Main.npc[npcTrack].CanBeChasedBy(base.Projectile))
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
			return;
		}
		base.Projectile.ai[0]++;
		if (!(base.Projectile.ai[0] >= 5f))
		{
			return;
		}
		int projXDirection = ((base.Projectile.SafeDirectionTo(Main.npc[npcTrack].Center, Vector2.UnitY).X > 0f) ? 1 : (-1));
		base.Projectile.direction = projXDirection;
		base.Projectile.ai[0] = -20f;
		base.Projectile.netUpdate = true;
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 attackPos = Main.npc[npcTrack].Center - base.Projectile.Center;
			for (int j = 0; j < 3; j++)
			{
				Vector2 finalAttackPos = base.Projectile.Center + attackPos.RotatedByRandom((j > 0) ? ((float)Math.PI / 8f) : 0f) * ((j > 0) ? Main.rand.NextFloat(0.9f, 1.1f) : 1f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), finalAttackPos, Vector2.Zero, ModContent.ProjectileType<SilvaCrystalExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, Main.rgbToHsl(new Color(Main.DiscoR, 203, 103)).X, base.Projectile.whoAmI);
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 127 - base.Projectile.alpha / 2);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 projPos = base.Projectile.Center + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Color colorArea = Lighting.GetColor((int)((double)base.Projectile.position.X + (double)base.Projectile.width * 0.5) / 16, (int)(((double)base.Projectile.position.Y + (double)base.Projectile.height * 0.5) / 16.0));
		Color colorAlpha = base.Projectile.GetAlpha(colorArea) * 0.2f;
		Vector2 origin = tex.Size() / 2f;
		float scaleFactor = MathF.Cos((float)Math.PI * 2f * (base.Projectile.localAI[0] / 60f)) + 6f;
		for (int k = 0; k < 4; k++)
		{
			Main.EntitySpriteDraw(tex, projPos + Vector2.UnitY.RotatedBy((float)k * ((float)Math.PI / 2f)) * scaleFactor, null, colorAlpha, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}
}
