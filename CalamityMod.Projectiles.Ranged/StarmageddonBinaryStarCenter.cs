using System;
using System.IO;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

[PierceResistException(false)]
public class StarmageddonBinaryStarCenter : ModProjectile, ILocalizedModType, IModType
{
	private const float TimeBeforeHoming = 30f;

	private const float FlamethrowerSoundFrequency = 30f;

	public const int StarDistanceFromCenter = 32;

	public const float SuckedProjectileDistanceFromStars = 600f;

	public const float SuckedProjectileSpawnRate = 15f;

	public const float StarRotationRate = 2f;

	public const float DustCloudSpawnRate = 16f;

	public const float DustCloudVelocityMax = 8f;

	public const float DustCloudSpreadMax = 2f;

	public const int ParticleStreamsPerStar = 2;

	public const float ParticleSpawnRate = 4f;

	public const float ParticleSpawnOffset = 19f;

	public const float ParticleVelocityMax = 32f;

	public const float ParticleSpreadMax = 8f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 38);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 3600;
		base.Projectile.penetrate = -1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		Projectile hostProjectile = Main.projectile[(int)base.Projectile.ai[0]];
		if (base.Projectile.type != ModContent.ProjectileType<StarmageddonBinaryStarCenter>() || !hostProjectile.active || hostProjectile.type != ModContent.ProjectileType<StarmageddonHeld>())
		{
			base.Projectile.Kill();
			return;
		}
		_ = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[0] == 0f && Main.myPlayer == base.Projectile.owner)
		{
			base.Projectile.localAI[0] = 1f;
			int starAmount = 2;
			int starSpread = 360 / starAmount;
			int starDistance = 32;
			Vector2 starSpawnPosition = default(Vector2);
			for (int i = 0; i < starAmount; i++)
			{
				((Vector2)(ref starSpawnPosition))._002Ector(base.Projectile.Center.X + (float)(Math.Sin(i * starSpread) * (double)starDistance), base.Projectile.Center.Y + (float)(Math.Cos(i * starSpread) * (double)starDistance));
				int projectileType = ((i == 0) ? ModContent.ProjectileType<StarmageddonStar>() : ModContent.ProjectileType<StarmageddonStar2>());
				int star = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), starSpawnPosition, Vector2.Zero, projectileType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, Projectile.GetByUUID(base.Projectile.owner, base.Projectile.whoAmI));
				Main.projectile[star].ai[1] = i * starSpread;
			}
		}
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.localAI[1]++;
			if (base.Projectile.localAI[1] % 30f == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item34, base.Projectile.Center);
			}
			int npcIndex = (int)base.Projectile.ai[2];
			NPC npc = Main.npc[npcIndex];
			bool findNewTarget = false;
			if (!npcIndex.WithinBounds(Main.maxNPCs))
			{
				findNewTarget = true;
			}
			else if (npc.active && !npc.dontTakeDamage)
			{
				base.Projectile.Center = npc.Center - base.Projectile.velocity * 2f;
				base.Projectile.gfxOffY = npc.gfxOffY;
			}
			else
			{
				findNewTarget = true;
			}
			if (findNewTarget)
			{
				base.Projectile.ai[1] = 0f;
				base.Projectile.ai[2] = 0f;
			}
		}
		else if (base.Projectile.localAI[1] < 30f)
		{
			base.Projectile.localAI[1]++;
		}
		else
		{
			NPC target = base.Projectile.FindTargetWithinRange(1600f);
			if (target != null)
			{
				base.Projectile.velocity = base.Projectile.SuperhomeTowardsTarget(target, 24f, 12f);
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Rectangle myRect = base.Projectile.Hitbox;
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
		{
			NPC npc = Main.npc[npcIndex];
			if (!npc.active || npc.dontTakeDamage || ((!base.Projectile.friendly || (npc.friendly && (npc.type != 22 || base.Projectile.owner >= 255 || !player.killGuide) && (npc.type != 54 || base.Projectile.owner >= 255 || !player.killClothier))) && (!base.Projectile.hostile || !npc.friendly || npc.dontTakeDamageFromHostiles)) || (base.Projectile.owner >= 0 && npc.immune[base.Projectile.owner] != 0 && base.Projectile.maxPenetrate != 1) || (!npc.noTileCollide && base.Projectile.ownerHitCheck))
			{
				continue;
			}
			bool stickingToNPC;
			if (npc.type == 414)
			{
				Rectangle rect = npc.Hitbox;
				int rectSizeChange = 8;
				rect.X -= rectSizeChange;
				rect.Y -= rectSizeChange;
				rect.Width += rectSizeChange * 2;
				rect.Height += rectSizeChange * 2;
				stickingToNPC = base.Projectile.Colliding(myRect, rect);
			}
			else
			{
				stickingToNPC = base.Projectile.Colliding(myRect, npc.Hitbox);
			}
			if (!stickingToNPC)
			{
				continue;
			}
			if (npc.reflectsProjectiles && base.Projectile.CanBeReflected())
			{
				npc.ReflectProjectile(base.Projectile);
				break;
			}
			base.Projectile.ai[1] = 1f;
			base.Projectile.ai[2] = npcIndex;
			base.Projectile.velocity = (npc.Center - base.Projectile.Center) * 0.75f;
			base.Projectile.netUpdate = true;
			Point[] array2 = (Point[])(object)new Point[5];
			int projCount = 0;
			for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
			{
				Projectile proj = Main.projectile[projIndex];
				if (projIndex != base.Projectile.whoAmI && proj.active && proj.owner == Main.myPlayer && proj.type == base.Projectile.type && proj.ai[0] == 1f && proj.ai[1] == (float)npcIndex)
				{
					array2[projCount++] = new Point(projIndex, proj.timeLeft);
					if (projCount >= array2.Length)
					{
						break;
					}
				}
			}
			if (projCount < array2.Length)
			{
				continue;
			}
			int maxProj = 0;
			for (int m = 1; m < array2.Length; m++)
			{
				if (array2[m].Y < array2[maxProj].Y)
				{
					maxProj = m;
				}
			}
			Main.projectile[array2[maxProj].X].Kill();
		}
	}
}
