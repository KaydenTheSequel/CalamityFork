using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crabulon;

public class CrabShroom : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.NPC.damage = 18;
		base.NPC.width = 14;
		base.NPC.height = 14;
		if (Main.getGoodWorld)
		{
			base.NPC.scale = 2f;
		}
		base.NPC.lifeMax = 15;
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 6000;
		}
		base.AIType = -1;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.NPC.Center, 0f, 0.2f, 0.4f);
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float xVelocityLimit = (death ? 8f : (revenge ? 6f : 5f));
		float yVelocityLimit = (Main.getGoodWorld ? 0.25f : (death ? 0.75f : (revenge ? 0.9f : 1f)));
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		base.NPC.velocity.Y += 0.02f;
		if (base.NPC.velocity.Y > yVelocityLimit)
		{
			base.NPC.velocity.Y = yVelocityLimit;
		}
		if (base.NPC.position.X + (float)base.NPC.width < player.position.X)
		{
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.velocity.X *= 0.98f;
			}
			base.NPC.velocity.X += 0.1f;
		}
		else if (base.NPC.position.X > player.position.X + (float)player.width)
		{
			if (base.NPC.velocity.X > 0f)
			{
				base.NPC.velocity.X *= 0.98f;
			}
			base.NPC.velocity.X -= 0.1f;
		}
		if (base.NPC.velocity.X > xVelocityLimit || base.NPC.velocity.X < 0f - xVelocityLimit)
		{
			base.NPC.velocity.X *= 0.97f;
		}
		base.NPC.rotation = base.NPC.velocity.X * 0.1f;
		if (!Main.getGoodWorld)
		{
			return;
		}
		float pushVelocity = 0.5f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && Vector2.Distance(base.NPC.Center, n.Center) < 30f * base.NPC.scale)
			{
				if (base.NPC.position.X < n.position.X)
				{
					base.NPC.velocity.X -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.X += pushVelocity;
				}
				if (base.NPC.position.Y < n.position.Y)
				{
					base.NPC.velocity.Y -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.Y += pushVelocity;
				}
			}
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.zenithWorld)
		{
			return null;
		}
		return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, (int)((Color)(ref drawColor)).A) * base.NPC.Opacity;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Texture2D glow = GlowTexture.Value;
		Color colorToShift = (Color)(Main.zenithWorld ? new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB) : Color.Cyan);
		Color glowColor = Color.Lerp(Color.White, colorToShift, 0.5f);
		int ClonesAroundShroom = (Main.zenithWorld ? 4 : 0);
		Vector2 drawOrigin = default(Vector2);
		for (int c = 0; c < 1 + ClonesAroundShroom; c++)
		{
			((Vector2)(ref drawOrigin))._002Ector((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[base.Type] / 2));
			Vector2 drawPos = base.NPC.Center - screenPos + (Vector2.UnitX * (float)texture.Width * (float)c).RotatedByRandom(c);
			drawPos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
			drawPos += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
			spriteBatch.Draw(texture, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
			spriteBatch.Draw(glow, drawPos, (Rectangle?)base.NPC.frame, glowColor, base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
		}
		return false;
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(8) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 2; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 56, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 56, hit.HitDirection, -1f);
			}
		}
	}
}
