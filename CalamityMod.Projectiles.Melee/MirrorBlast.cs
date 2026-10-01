using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MirrorBlast : ModProjectile, ILocalizedModType, IModType
{
	public bool isShard = true;

	public int shardShield;

	private bool hasSpawned;

	public int shardNum = -1;

	public new string LocalizationCategory => "Projectiles.Melee";

	public bool isShield => shardShield > 0;

	private Player player => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 1800;
		base.Projectile.extraUpdates = 0;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.tileCollide = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(isShard);
		writer.Write(hasSpawned);
		writer.Write(shardNum);
		writer.Write(shardShield);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		isShard = reader.ReadBoolean();
		hasSpawned = reader.ReadBoolean();
		shardNum = reader.ReadInt32();
		shardShield = reader.ReadInt32();
	}

	public override bool? CanDamage()
	{
		return !isShard;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target.Calamity().DR > 0.9f)
		{
			return false;
		}
		return base.CanHitNPC(target);
	}

	public override void AI()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().mouseWorldListener = true;
		if (!hasSpawned)
		{
			base.Projectile.netUpdate = true;
			if (base.Projectile.ai[0] != 0f)
			{
				base.Projectile.ArmorPenetration = 1000;
				hasSpawned = true;
				shardShield = 0;
				isShard = false;
				base.Projectile.timeLeft = 1200;
				base.Projectile.velocity = base.Projectile.DirectionTo(player.Center) * -20f;
				base.Projectile.DamageType = DamageClass.Generic;
				base.Projectile.CritChance = 0;
				return;
			}
			shardNum = player.ownedProjectileCounts[base.Projectile.type];
			hasSpawned = true;
			shardNum = 0;
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.type == ModContent.ProjectileType<MirrorBlast>() && proj.owner == base.Projectile.owner)
				{
					(proj.ModProjectile as MirrorBlast).shardNum++;
					proj.netUpdate = true;
				}
			}
		}
		if (isShield)
		{
			if (shardNum > 10 || base.Projectile.timeLeft < 2)
			{
				shardShield = 0;
				isShard = false;
				base.Projectile.timeLeft = 1200;
				base.Projectile.velocity = base.Projectile.DirectionTo(player.Center) * -20f;
				base.Projectile.netUpdate = true;
				return;
			}
			List<Vector2> positions = new List<Vector2>
			{
				new Vector2(0f, 75f),
				new Vector2(10f, 65f),
				new Vector2(-10f, 65f),
				new Vector2(20f, 75f),
				new Vector2(-20f, 75f),
				new Vector2(30f, 65f),
				new Vector2(-30f, 65f),
				new Vector2(14f, 55f),
				new Vector2(-14f, 55f),
				new Vector2(0f, 45f)
			};
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, player.Center + player.DirectionTo(player.Calamity().mouseWorld).RotatedBy(MathHelper.ToRadians(positions[shardNum - 1].X)) * positions[shardNum - 1].Y, 0.5f);
			base.Projectile.rotation = base.Projectile.DirectionTo(player.Center).ToRotation() + (float)Math.PI / 2f;
			shardShield--;
		}
		else if (isShard)
		{
			base.Projectile.velocity = Vector2.Zero;
			int shardCount = 0;
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj2 in projectile)
			{
				if (proj2.active && proj2.type == ModContent.ProjectileType<MirrorBlast>() && proj2.owner == base.Projectile.owner && (proj2.ModProjectile as MirrorBlast).isShard)
				{
					shardCount++;
				}
			}
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, player.Center + Utils.RotatedBy(new Vector2(0f, MathHelper.Lerp(-90f, -110f, MathF.Sin((float)player.miscCounter / 300f * ((float)Math.PI * 2f) + (float)Math.PI * (float)(shardNum % 2)))), (double)MathHelper.ToRadians((float)player.miscCounter / 300f * 360f + 360f / (float)shardCount * (float)shardNum), default(Vector2)), 0.15f);
			base.Projectile.rotation = base.Projectile.DirectionTo(player.Center).ToRotation() + (float)Math.PI / 2f;
			if (shardNum > 10 || base.Projectile.timeLeft < 2)
			{
				isShard = false;
				base.Projectile.timeLeft = 1200;
				base.Projectile.velocity = base.Projectile.DirectionTo(player.Center) * -20f;
			}
		}
		else
		{
			float homingStrength = 0.025f;
			if (base.Projectile.timeLeft < 1000)
			{
				homingStrength *= 2f;
			}
			if (base.Projectile.timeLeft < 800)
			{
				homingStrength *= 2f;
			}
			if (base.Projectile.timeLeft < 600)
			{
				homingStrength *= 2f;
			}
			NPC target = FindClosestNPC(3200f);
			if (target != null)
			{
				Vector2 direction = target.Center - base.Projectile.Center;
				((Vector2)(ref direction)).Normalize();
				direction *= 40f;
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, direction, homingStrength);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * Main.rand.NextFloat(-0.2f, -0.6f), Color.Black, 7, Main.rand.NextFloat(0.35f, 0.4f), 1f, Main.rand.NextFloat(-0.2f, 0.2f)));
				if (Main.rand.NextBool(5))
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f) - base.Projectile.velocity, 66);
					dust.scale = Main.rand.NextFloat(0.7f, 0.85f);
					dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.5f);
					dust.color = (Main.rand.NextBool() ? Color.AliceBlue : Color.SkyBlue);
					dust.noGravity = true;
				}
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		Lighting.AddLight(base.Projectile.Center, 0.3168f, 0.30030003f, 0.33f);
		if (base.Projectile.FinalExtraUpdate())
		{
			base.Projectile.frameCounter++;
		}
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 3)
		{
			base.Projectile.frame = 0;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (!isShard)
		{
			SoundStyle style = SoundID.Item27 with
			{
				Volume = 0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Vector2.Zero, Color.WhiteSmoke, Color.BlueViolet, Main.rand.NextFloat(1.5f, 1.6f), 10, 0.1f, 3f));
			for (int i = 0; i < 10; i++)
			{
				int dust = Dust.NewDust(base.Projectile.Center - base.Projectile.velocity / 2f, 0, 0, 91, 0f, 0f, 100);
				Dust obj = Main.dust[dust];
				obj.velocity *= 2f;
				Main.dust[dust].noGravity = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		Texture2D BlastTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MirrorBlast", (AssetRequestMode)2).Value;
		Texture2D ShardTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MirrorShard", (AssetRequestMode)2).Value;
		Point BlastTextureDim = default(Point);
		((Point)(ref BlastTextureDim))._002Ector(60, 26);
		Point ShardTextureDim = default(Point);
		((Point)(ref ShardTextureDim))._002Ector(32, 14);
		Texture2D UsedTex = (isShard ? ShardTex : BlastTex);
		Point UsedTextureDim = (isShard ? ShardTextureDim : BlastTextureDim);
		Vector2 origin = (isShard ? (ShardTextureDim.ToVector2() / 2f) : (BlastTextureDim.ToVector2() / 2f + new Vector2(14f, 0f)));
		Main.spriteBatch.Draw(UsedTex, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, UsedTextureDim.Y * base.Projectile.frame, UsedTextureDim.X, UsedTextureDim.Y), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	private NPC FindClosestNPC(float maxRange)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		NPC closestNPC = null;
		float closestDistance = maxRange;
		NPC[] npc = Main.npc;
		foreach (NPC npc2 in npc)
		{
			if (npc2.CanBeChasedBy(this) && !npc2.friendly)
			{
				float distance = Vector2.Distance(base.Projectile.Center, npc2.Center);
				if (distance < closestDistance)
				{
					closestDistance = distance;
					closestNPC = npc2;
				}
			}
		}
		return closestNPC;
	}
}
