using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SmallSkeletonMinion : ModProjectile, ILocalizedModType, IModType
{
	public int Variant;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 34;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		Projectile projectile = base.Projectile;
		int aiStyle = (base.AIType = -1);
		projectile.aiStyle = aiStyle;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 15;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Variant);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Variant = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			Variant = Main.rand.Next(3);
			base.Projectile.netUpdate = true;
			base.Projectile.localAI[0]++;
		}
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 6f == 5f)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame < 1)
		{
			base.Projectile.frame = 1;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 1;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<SmallSkeletonMinion>();
		player.AddBuff(ModContent.BuffType<SmallSkeletonBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.necrosteocytesDudes = false;
			}
			if (modPlayer.necrosteocytesDudes)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(900f, player);
		if (base.Projectile.velocity.Y == 0f && (HoleBelow() || (base.Projectile.Distance(player.Center) > 205f && base.Projectile.position.X == base.Projectile.oldPosition.X)))
		{
			base.Projectile.velocity.Y = -10f;
		}
		else if (base.Projectile.velocity.Y != 0f)
		{
			base.Projectile.frame = 2;
		}
		if (base.Projectile.velocity.Y > -16f)
		{
			base.Projectile.velocity.Y += 0.3f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] % 60f == 59f && Main.myPlayer == base.Projectile.owner)
		{
			int type = Utils.SelectRandom<int>(Main.rand, ModContent.ProjectileType<BoneMatter>(), ModContent.ProjectileType<BoneMatter2>());
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (potentialTarget == null)
		{
			if (Math.Abs(player.Center.X - base.Projectile.Center.X + 40f * (float)base.Projectile.minionPos) > 160f)
			{
				base.Projectile.velocity.X += Main.rand.NextFloat(0.11f, 0.16f) * (float)(player.Center.X - base.Projectile.Center.X + 40f * (float)base.Projectile.minionPos > 0f).ToDirectionInt();
				base.Projectile.velocity.X = MathHelper.Clamp(base.Projectile.velocity.X, -13f, 13f);
			}
			else
			{
				base.Projectile.velocity.X *= 0.95f;
			}
			if (base.Projectile.Distance(player.Center) <= 150f)
			{
				base.Projectile.frame = 0;
			}
			if (base.Projectile.Distance(player.Center) > 1600f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
		}
		else
		{
			if (Math.Abs(potentialTarget.Center.X - base.Projectile.Center.X + (float)(int)MathHelper.Min((float)(potentialTarget.width / 2), 40f)) > (float)(int)MathHelper.Min((float)(potentialTarget.width / 2), 40f))
			{
				base.Projectile.velocity.X += Main.rand.NextFloat(0.08f, 0.15f) * (float)(potentialTarget.Center.X - base.Projectile.Center.X > 0f).ToDirectionInt();
				base.Projectile.velocity.X = MathHelper.Clamp(base.Projectile.velocity.X, -16f, 16f);
			}
			else
			{
				base.Projectile.velocity.X *= 0.95f;
			}
			if (base.Projectile.Distance(player.Center) > 1400f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
			base.Projectile.MinionAntiClump(0.075f);
		}
		if (base.Projectile.velocity.X > 0.25f)
		{
			base.Projectile.spriteDirection = 1;
		}
		else if (base.Projectile.velocity.X < -0.25f)
		{
			base.Projectile.spriteDirection = -1;
		}
	}

	public bool HoleBelow()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		int tileWidth = 4;
		int tileX = (int)(base.Projectile.Center.X / 16f) - tileWidth;
		if (base.Projectile.velocity.X > 0f)
		{
			tileX += tileWidth;
		}
		int tileY = (int)((base.Projectile.position.Y + (float)base.Projectile.height) / 16f);
		for (int y = tileY; y < tileY + 2; y++)
		{
			for (int x = tileX; x < tileX + tileWidth; x++)
			{
				if (Main.tile[x, y].HasTile)
				{
					return false;
				}
			}
		}
		return true;
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(Variant * base.Projectile.width, base.Projectile.frame * base.Projectile.height, base.Projectile.width, base.Projectile.height);
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Main.EntitySpriteDraw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, spriteEffects);
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		fallThrough = base.Projectile.Bottom.Y < Main.player[base.Projectile.owner].Top.Y - 120f;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
