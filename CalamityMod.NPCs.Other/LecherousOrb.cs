using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Other;

public class LecherousOrb : ModNPC
{
	public ref float Time => ref base.NPC.ai[0];

	public ref float Frame => ref base.NPC.localAI[0];

	public Player Owner
	{
		get
		{
			if (base.NPC.target >= 255 || base.NPC.target < 0)
			{
				base.NPC.TargetClosest();
			}
			return Main.player[base.NPC.target];
		}
	}

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.width = (base.NPC.height = 28);
		base.NPC.damage = 0;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 180000;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath7;
		base.NPC.knockBackResist = 0f;
		base.NPC.netAlways = true;
		base.NPC.aiStyle = -1;
		base.NPC.canGhostHeal = false;
		base.NPC.Calamity().ProvidesProximityRage = false;
		base.NPC.Calamity().DoesNotDisappearInBossRush = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = 180000;
	}

	public override void AI()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.Opacity = Utils.GetLerpValue(0f, 15f, Time, clamped: true);
		base.NPC.velocity = Vector2.Zero;
		if (Main.myPlayer == base.NPC.target)
		{
			if (!Owner.Calamity().lecherousOrbEnchant)
			{
				base.NPC.active = false;
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.checkDead();
				base.NPC.netUpdate = true;
			}
			Owner.Calamity().awaitingLecherousOrbSpawn = false;
			Vector2 destination = Vector2.Lerp(Owner.Center, Owner.ClampedMouseWorld(), 0.625f);
			base.NPC.Center = Vector2.Lerp(base.NPC.Center, destination, 0.035f).MoveTowards(destination, 8f);
			if (base.NPC.WithinRange(destination, 5f))
			{
				base.NPC.Center = destination;
			}
			if (!base.NPC.WithinRange(destination, 2000f))
			{
				base.NPC.Center = Owner.Center;
			}
			bool wasNotAtDestinationBefore = Vector2.Distance(base.NPC.position + base.NPC.Size * 0.5f, destination) < 0.1f && Vector2.Distance(base.NPC.oldPosition + base.NPC.Size * 0.5f, destination) > 0.1f;
			if ((Vector2.Distance(base.NPC.position, base.NPC.oldPosition) > 30f) | wasNotAtDestinationBefore)
			{
				base.NPC.SyncMotionToServer();
			}
		}
		if (Frame == 0f && Main.rand.NextBool(150))
		{
			Frame = 1f;
		}
		if (Frame == 0f && Main.rand.NextBool(300))
		{
			Frame = 8f;
		}
		base.NPC.spriteDirection = (Owner.Center.X > base.NPC.Center.X).ToDirectionInt();
		Time++;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Circular(12f, 12f), 264);
			dust.color = Color.Red;
			dust.velocity = Main.rand.NextVector2Circular(3f, 3f);
			dust.fadeIn = 0.9f;
			dust.scale = 1.3f;
			dust.noGravity = true;
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int j = 0; j < 15; j++)
		{
			Dust dust2 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Circular(12f, 12f), 264);
			dust2.color = Color.Red;
			dust2.velocity = Main.rand.NextVector2Circular(6f, 6f);
			dust2.fadeIn = 1.25f;
			dust2.scale = Main.rand.NextFloat(1.2f, 1.56f);
			dust2.noGravity = true;
		}
		if (!Main.dedServ)
		{
			for (int k = 1; k <= 4; k++)
			{
				Gore.NewGoreDirect(base.NPC.GetSource_Death(), base.NPC.Center, Main.rand.NextVector2Circular(3f, 3f), base.Mod.Find<ModGore>($"LecherousGore{k}").Type);
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (Frame != 0f)
		{
			base.NPC.frameCounter++;
		}
		else
		{
			base.NPC.frameCounter = 0.0;
		}
		if (Frame >= 1f && Frame < 8f && base.NPC.frameCounter % 6.0 == 5.0)
		{
			Frame++;
			if (Frame >= 8f)
			{
				Frame = 0f;
			}
		}
		if (Frame >= 8f && Frame < 25f && base.NPC.frameCounter % 5.0 == 4.0)
		{
			Frame++;
			if (Frame >= 25f)
			{
				Frame = 0f;
			}
		}
		int verticalFrame = (int)Frame % 22;
		int horizontalFrame = (int)Frame / 22;
		base.NPC.frame.X = horizontalFrame * 64;
		base.NPC.frame.Y = verticalFrame * 90;
		base.NPC.frame.Width = 64;
		base.NPC.frame.Height = 90;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 drawPosition = base.NPC.Center - screenPos;
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection != 1);
		float pulse = Main.GlobalTimeWrappedHourly * 1.9f % 1f;
		float pulseScale = base.NPC.scale * (1f + pulse * 0.33f);
		Color pulseColor = base.NPC.GetAlpha(Color.Red) * (1f - pulse) * 0.44f;
		spriteBatch.Draw(texture, drawPosition, (Rectangle?)base.NPC.frame, pulseColor, base.NPC.rotation, base.NPC.frame.Size() * 0.5f, pulseScale, direction, 0f);
		spriteBatch.Draw(texture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		return false;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		if (Main.myPlayer == base.NPC.target)
		{
			base.NPC.SyncMotionToServer();
		}
	}

	public override void OnKill()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		int heartsToGive = (int)MathHelper.Lerp(0f, 7f, Utils.GetLerpValue(45f, 540f, Time, clamped: true));
		for (int i = 0; i < heartsToGive; i++)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), base.NPC.Hitbox, 58);
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * base.NPC.Opacity;
	}
}
