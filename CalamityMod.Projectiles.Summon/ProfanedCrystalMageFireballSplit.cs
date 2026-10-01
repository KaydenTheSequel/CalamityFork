using System;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ProfanedCrystalMageFireballSplit : ModProjectile, ILocalizedModType, IModType
{
	private int damage;

	private int hits;

	private NPC target;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/HolyFire2";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.SummonTagDamageMultiplier[base.Type] = 0.3f;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.minion = true;
		base.Projectile.gfxOffY = -25f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool PreAI()
	{
		base.Projectile.Calamity().overridesMinionDamagePrevention = true;
		return true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.1f, 0f);
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		if (base.Projectile.timeLeft == 600)
		{
			damage = base.Projectile.damage;
			base.Projectile.damage = 0;
		}
		if (base.Projectile.timeLeft > 550)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		int num469 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, ProvUtils.GetDustID(!Main.dayTime), 0f, 0f, 100, default(Color), Main.dayTime ? 1f : 0.75f);
		Main.dust[num469].noGravity = true;
		Dust obj = Main.dust[num469];
		obj.velocity *= 0f;
		if (base.Projectile.timeLeft > 550)
		{
			return;
		}
		if (base.Projectile.penetrate == -1)
		{
			base.Projectile.damage = damage;
		}
		base.Projectile.penetrate = 1;
		if (base.Projectile.timeLeft > 500)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.06f;
		}
		float num535 = base.Projectile.position.X;
		float num536 = base.Projectile.position.Y;
		float num537 = 3000f;
		bool flag19 = false;
		NPC ownerMinionAttackTargetNPC2 = base.Projectile.OwnerMinionAttackTargetNPC;
		if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(base.Projectile))
		{
			float num539 = ownerMinionAttackTargetNPC2.position.X + (float)(ownerMinionAttackTargetNPC2.width / 2);
			float num540 = ownerMinionAttackTargetNPC2.position.Y + (float)(ownerMinionAttackTargetNPC2.height / 2);
			float num541 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - num539) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - num540);
			if (num541 < num537)
			{
				num537 = num541;
				num535 = num539;
				num536 = num540;
				flag19 = true;
				target = ownerMinionAttackTargetNPC2;
			}
		}
		if (!flag19)
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].CanBeChasedBy(base.Projectile))
				{
					float num543 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
					float num544 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
					float num545 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - num543) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - num544);
					if (num545 < num537)
					{
						num537 = num545;
						num535 = num543;
						num536 = num544;
						flag19 = true;
						target = Main.npc[i];
					}
				}
			}
		}
		if (flag19 && base.Projectile.ai[1] == 0f)
		{
			float num550 = 24f;
			Vector2 vector43 = base.Projectile.Center;
			float num551 = num535 - vector43.X;
			float num552 = num536 - vector43.Y;
			float num553 = (float)Math.Sqrt(num551 * num551 + num552 * num552);
			if (num553 < 100f)
			{
				num550 = 28f;
			}
			num553 = num550 / num553;
			num551 *= num553;
			num552 *= num553;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 14f + num551) / 15f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 14f + num552) / 15f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (Main.dayTime ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HolyFire2Night", (AssetRequestMode)2).Value);
		int num214 = texture.Height / Main.projFrames[base.Type];
		int y6 = num214 * base.Projectile.frame;
		base.Projectile.DrawBackglow(ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha, Outline: true), 4f, texture, null, (SpriteEffects)0);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, num214), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num214 / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[base.Projectile.owner].Calamity().rollBabSpears(35, target.chaseable);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		Main.player[base.Projectile.owner].Calamity().rollBabSpears(35, chaseable: true);
	}

	public override bool? CanHitNPC(NPC target)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (target.type != ModContent.NPCType<SepulcherHead>() && target.type != ModContent.NPCType<SepulcherBody>() && target.type != ModContent.NPCType<SepulcherBodyEnergyBall>() && target.type != ModContent.NPCType<SepulcherTail>() && this.target != null && target != this.target)
		{
			Rectangle rect = base.Projectile.getRect();
			if (((Rectangle)(ref rect)).Intersects(target.getRect()))
			{
				hits++;
				if (hits >= 25)
				{
					base.Projectile.Kill();
				}
			}
			return false;
		}
		return null;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = (base.Projectile.height = 200);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		int dust = ProvUtils.GetDustID(!Main.dayTime);
		for (int num621 = 0; num621 < 4; num621++)
		{
			int num622 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 2f : 0.5f);
			Dust obj = Main.dust[num622];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[num622].scale = 0.5f;
				Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 12; i++)
		{
			int num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 3f : 0.75f);
			Main.dust[num624].noGravity = true;
			Dust obj2 = Main.dust[num624];
			obj2.velocity *= 5f;
			num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 2f : 0.5f);
			Dust obj3 = Main.dust[num624];
			obj3.velocity *= 2f;
		}
	}
}
