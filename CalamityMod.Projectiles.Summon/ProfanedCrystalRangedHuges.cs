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

public class ProfanedCrystalRangedHuges : ModProjectile, ILocalizedModType, IModType
{
	private bool boomerSwarm;

	private bool kill;

	private NPC target;

	public new string LocalizationCategory => "Projectiles.Summon";

	private void swarmAI()
	{
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		float num535 = base.Projectile.position.X;
		float num536 = base.Projectile.position.Y;
		float num537 = 2000f;
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
		if (flag19)
		{
			base.Projectile.tileCollide = false;
			if (base.Projectile.timeLeft < 20)
			{
				base.Projectile.timeLeft = 50;
				kill = true;
			}
			float num550 = 40f;
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
		else
		{
			if (kill)
			{
				base.Projectile.Kill();
			}
			base.Projectile.tileCollide = true;
		}
	}

	private void swarm(Vector2 targetPos)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		float swarmAmount = Main.rand.Next(6, 10);
		Vector2 pos = default(Vector2);
		for (float i = 0f; i < swarmAmount; i++)
		{
			float x = targetPos.X + (float)Main.rand.Next(-500, 501);
			float y = targetPos.Y - 500f + (float)Main.rand.Next(-200, 1);
			((Vector2)(ref pos))._002Ector(x, y);
			Vector2 correctedVelocity = base.Projectile.position - pos;
			((Vector2)(ref correctedVelocity)).Normalize();
			correctedVelocity *= 25f;
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, correctedVelocity, ModContent.ProjectileType<ProfanedCrystalRangedHuges>(), (int)((double)base.Projectile.damage * 1.5), base.Projectile.knockBack, base.Projectile.owner, 2f);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].originalDamage = (int)((double)base.Projectile.originalDamage * 1.5);
				Main.projectile[proj].DamageType = DamageClass.Summon;
			}
			((ProfanedCrystalRangedHuges)Main.projectile[proj].ModProjectile).boomerSwarm = true;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.SummonTagDamageMultiplier[base.Type] = 0.4f;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 175;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override bool PreAI()
	{
		base.Projectile.Calamity().overridesMinionDamagePrevention = true;
		bool begin = base.Projectile.timeLeft == 175;
		if ((base.Projectile.ai[0] >= 1f) & begin)
		{
			base.Projectile.scale = 1.5f;
			base.Projectile.width += 25;
			base.Projectile.height += 25;
			boomerSwarm = base.Projectile.ai[0] == 2f;
			if (boomerSwarm && base.Projectile.ai[1] == 0f)
			{
				base.Projectile.timeLeft = 200;
				base.Projectile.ai[1] = 1f;
			}
		}
		return true;
	}

	public override void AI()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 175 && base.Projectile.scale == 1.5f)
		{
			SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, base.Projectile.Center);
		}
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 2)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft == 145 && base.Projectile.ai[0] < 2f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= (boomerSwarm ? 1.03f : 1.02f);
		int dust = ProvUtils.GetDustID(!Main.dayTime);
		int num469 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100);
		Main.dust[num469].noGravity = true;
		Dust obj = Main.dust[num469];
		obj.velocity *= 0f;
		if (boomerSwarm)
		{
			swarmAI();
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
		Texture2D texture = (Main.dayTime ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/ProfanedCrystalRangedHugesNight", (AssetRequestMode)2).Value);
		int num214 = texture.Height / Main.projFrames[base.Type];
		int y6 = num214 * base.Projectile.frame;
		base.Projectile.DrawBackglow(ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha, Outline: true), 4f, texture, null, (SpriteEffects)0);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, num214), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num214 / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target.type != ModContent.NPCType<SepulcherHead>() && target.type != ModContent.NPCType<SepulcherBody>() && target.type != ModContent.NPCType<SepulcherBodyEnergyBall>() && target.type != ModContent.NPCType<SepulcherTail>() && this.target != null && target != this.target)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Main.player[base.Projectile.owner].Calamity().rollBabSpears((base.Projectile.ai[0] == 1f) ? 1 : 10, target.chaseable);
		if (base.Projectile.scale == 1.5f && base.Projectile.ai[0] != 2f)
		{
			swarm(target.Center);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Main.player[base.Projectile.owner].Calamity().rollBabSpears((base.Projectile.scale == 1.5f) ? 1 : 10, chaseable: true);
		if (base.Projectile.scale == 1.5f && base.Projectile.ai[0] != 2f)
		{
			swarm(target.Center);
		}
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
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
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
		if (base.Projectile.scale != 1.5f || Main.dedServ)
		{
			return;
		}
		for (int j = 0; j < 3; j++)
		{
			float scaleFactor10 = 0.33f;
			if (j == 1)
			{
				scaleFactor10 = 0.66f;
			}
			if (j == 2)
			{
				scaleFactor10 = 1f;
			}
			for (int k = 0; k < 4; k++)
			{
				int num626 = Gore.NewGore(base.Projectile.GetSource_Death(), new Vector2(base.Projectile.position.X + (float)(base.Projectile.width / 2) - 24f, base.Projectile.position.Y + (float)(base.Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 0.75f);
				Gore gore = Main.gore[num626];
				gore.velocity *= scaleFactor10;
				if (k == 0 || k == 2)
				{
					gore.velocity.X++;
				}
				else
				{
					gore.velocity.X--;
				}
				if (k < 2)
				{
					gore.velocity.Y++;
				}
				else
				{
					gore.velocity.Y--;
				}
			}
		}
	}
}
