using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class CannonballJellyfish : ModNPC
{
	public bool hasBeenHit;

	public bool dying;

	public bool shouldTarget;

	public int boomTimer = -1;

	public int currentFrame;

	public const int dyingDuration = 75;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 50;
		base.NPC.width = 54;
		base.NPC.height = 76;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 400;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 100;
		base.NPC.value = Item.buyPrice(0, 0, 4);
		base.NPC.HitSound = SoundID.NPCHit25;
		base.NPC.DeathSound = SoundID.NPCDeath28;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CannonballJellyfishBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer1Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CannonballJellyfish")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(shouldTarget);
		writer.Write(boomTimer);
		writer.Write(currentFrame);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		shouldTarget = reader.ReadBoolean();
		boomTimer = reader.ReadInt32();
		currentFrame = reader.ReadInt32();
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		if (!dying)
		{
			return base.CanHitPlayer(target, ref cooldownSlot);
		}
		return false;
	}

	private void DyingAI()
	{
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		if (boomTimer == 0)
		{
			if (shouldTarget)
			{
				Explode();
				return;
			}
			base.NPC.alpha = 50;
			shouldTarget = true;
			boomTimer = 75;
			base.NPC.noTileCollide = true;
			base.NPC.netUpdate = true;
		}
		if (boomTimer == 60 && !shouldTarget)
		{
			base.NPC.velocity.Y = 0.75f;
		}
		if (shouldTarget)
		{
			if (base.NPC.target < 0 || !base.NPC.target.WithinBounds(255) || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.TargetClosest();
			}
			if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
			{
				base.NPC.TargetClosest();
			}
			Player player = Main.player[base.NPC.target];
			if (boomTimer >= 55)
			{
				float targetXDirection = player.position.X + (float)(player.width / 2);
				float num = player.position.Y + (float)(player.height / 2);
				float homingSpeed = 30f;
				Vector2 npcPosition = default(Vector2);
				((Vector2)(ref npcPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float targetXDist = targetXDirection - npcPosition.X;
				float targetYDist = num - npcPosition.Y;
				float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
				if (targetDistance < 100f)
				{
					homingSpeed = 10f;
				}
				targetDistance = homingSpeed / targetDistance;
				targetXDist *= targetDistance;
				targetYDist *= targetDistance;
				base.NPC.velocity.X = (base.NPC.velocity.X * 5f + targetXDist) / 6f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 5f + targetYDist) / 6f;
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + 1.57f;
			}
			if (Collision.DrownCollision(base.NPC.position, base.NPC.width, base.NPC.height) && !Collision.SolidCollision(base.NPC.Center, base.NPC.width, base.NPC.height))
			{
				Rectangle hitbox = base.NPC.Hitbox;
				if (!((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
				{
					goto IL_035e;
				}
			}
			Explode();
		}
		else
		{
			Lighting.AddLight(base.NPC.Center, (float)(175 - base.NPC.alpha) * 0.2f, 0f, 0f);
		}
		goto IL_035e;
		IL_035e:
		boomTimer--;
	}

	public override void AI()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.chaseable = !dying && hasBeenHit;
		if (dying)
		{
			DyingAI();
			return;
		}
		if (base.NPC.life <= 1)
		{
			dying = true;
			boomTimer = ((boomTimer == -1) ? 60 : boomTimer);
			base.NPC.life = 1;
			base.NPC.dontTakeDamage = true;
			base.NPC.netUpdate = true;
			return;
		}
		Lighting.AddLight(base.NPC.Center, (float)(67 - base.NPC.alpha) * 1f / 220f, (float)(218 - base.NPC.alpha) * 1f / 220f, (float)(166 - base.NPC.alpha) * 1f / 220f);
		if (base.NPC.justHit)
		{
			hasBeenHit = true;
			base.NPC.chaseable = true;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			base.NPC.velocity.Y = -6f;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.wet)
		{
			base.NPC.noGravity = true;
			if (base.NPC.localAI[2] > 0f)
			{
				base.NPC.localAI[2]--;
			}
			if (base.NPC.localAI[2] <= 0f)
			{
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.localAI[1]++;
				}
				else
				{
					base.NPC.localAI[1] = 0f;
				}
			}
			base.NPC.velocity.Y += 0.1f;
			if (currentFrame == 3 && base.NPC.frameCounter == 0.0)
			{
				base.NPC.velocity.Y = -1.35f;
			}
		}
		else
		{
			base.NPC.noGravity = false;
			base.NPC.velocity.Y = 2f;
			base.NPC.localAI[2] = 75f;
			base.NPC.netUpdate = true;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 7.0)
		{
			currentFrame = ((currentFrame != 3) ? (currentFrame + 1) : 0);
			base.NPC.netUpdate = true;
			base.NPC.frameCounter = 0.0;
		}
		base.NPC.frame.Y = currentFrame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer1 && spawnInfo.Water)
		{
			return SpawnCondition.CaveJellyfish.Chance * 1.1f;
		}
		return 0f;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Vector2 val = new Vector2(base.NPC.Center.X, base.NPC.Center.Y);
		Asset<Texture2D> texture = TextureAssets.Npc[base.Type];
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture.Value.Width / 2), (float)(texture.Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 vector = val - screenPos;
		Asset<Texture2D> glowTexture = TextureAssets.Npc[base.Type];
		vector -= new Vector2((float)glowTexture.Value.Width, (float)(glowTexture.Value.Height / Main.npcFrameCount[base.Type])) * 1f / 2f;
		vector += halfSizeTexture * 1f + new Vector2(0f, 4f + base.NPC.gfxOffY);
		Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), new Color(67, 218, 166));
		Main.spriteBatch.Draw(TextureAssets.Npc[base.Type].Value, vector, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, 1f, spriteEffects, 0f);
	}

	private void Explode()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.position = base.NPC.Center;
		base.NPC.height *= 3;
		base.NPC.width = base.NPC.height;
		NPC nPC = base.NPC;
		nPC.position -= base.NPC.Size * 0.5f;
		SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (!player.dead)
			{
				Rectangle hitbox = base.NPC.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
				{
					player.Hurt(PlayerDeathReason.ByNPC(base.NPC.whoAmI), base.NPC.damage * 3, base.NPC.direction);
				}
			}
		}
		for (int k = 0; k < 25; k++)
		{
			int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 323, 0f, -1f, 0, default(Color), 2f);
			if (Main.dust.IndexInRange(dust))
			{
				Main.dust[dust].noGravity = true;
			}
		}
		base.NPC.active = false;
		base.NPC.NPCLoot();
		base.NPC.netUpdate = true;
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		target.AddBuff(20, 60 * (shouldTarget ? 4 : 2));
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 60 * (shouldTarget ? 4 : 2));
	}

	public override bool CheckDead()
	{
		base.NPC.life = 1;
		base.NPC.alpha = ((boomTimer == -1) ? 125 : base.NPC.alpha);
		boomTimer = ((boomTimer == -1) ? 60 : boomTimer);
		dying = true;
		base.NPC.active = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.netUpdate = true;
		return false;
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		if (!dying)
		{
			return base.CanBeHitByItem(player, item);
		}
		return false;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (!dying)
		{
			return base.CanBeHitByProjectile(projectile);
		}
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 323, hit.HitDirection, -1f);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(166);
		npcLoot.Add(ItemDropRule.NormalvsExpert(887, 100, 50));
		npcLoot.Add(1303, 30);
	}
}
