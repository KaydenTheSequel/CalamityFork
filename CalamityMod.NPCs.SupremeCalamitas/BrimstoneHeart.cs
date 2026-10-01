using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

[HasPierceResist(false)]
public class BrimstoneHeart : ModNPC
{
	public List<Vector2> ChainEndpoints = new List<Vector2>();

	public int ChainHeartIndex => (int)base.NPC.ai[0];

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		Main.npcFrameCount[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 24;
		base.NPC.height = 24;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 15000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.hide = true;
		base.NPC.HitSound = SoundID.NPCHit13;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(ChainEndpoints.Count);
		for (int i = 0; i < ChainEndpoints.Count; i++)
		{
			writer.WriteVector2(ChainEndpoints[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		ChainEndpoints.Clear();
		int endpointCount = reader.ReadInt32();
		for (int i = 0; i < endpointCount; i++)
		{
			ChainEndpoints.Add(reader.ReadVector2());
		}
	}

	public override void AI()
	{
		if (CalamityGlobalNPC.SCal < 0 || !Main.npc[CalamityGlobalNPC.SCal].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		base.NPC.alpha -= 42;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(4) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float widthInterpolant = Utils.GetLerpValue(0f, 0.16f, completionRatio, clamped: true) * Utils.GetLerpValue(1f, 0.84f, completionRatio, clamped: true);
		widthInterpolant = (float)Math.Pow(widthInterpolant, 8.0);
		float num = MathHelper.Lerp(4f, 1f, widthInterpolant);
		float pulseWidth = MathHelper.Lerp(0f, 3.2f, (float)Math.Pow(Math.Sin(Main.GlobalTimeWrappedHourly * 2.6f + (float)base.NPC.whoAmI * 1.3f + completionRatio), 16.0));
		return num + pulseWidth;
	}

	public Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		float colorInterpolant = MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(0f, 0.34f, completionRatio, clamped: true) * Utils.GetLerpValue(1.07f, 0.66f, completionRatio, clamped: true));
		return Color.Lerp(Color.DarkRed * 0.7f, Color.Red, colorInterpolant) * 0.425f;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		int frame = (int)Math.Round((float)Math.Pow(Math.Sin(Main.GlobalTimeWrappedHourly * 2.6f + (float)base.NPC.whoAmI * 1.3f), 6.0) * (float)Main.npcFrameCount[base.Type]);
		if (frame >= Main.npcFrameCount[base.Type])
		{
			frame = Main.npcFrameCount[base.Type] - 1;
		}
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		spriteBatch.ExitShaderRegion();
		for (int i = 0; i < ChainEndpoints.Count; i++)
		{
			float dist = base.NPC.Distance(ChainEndpoints[i]);
			List<Vector2> points = new List<Vector2>();
			for (int j = 0; j < 4; j++)
			{
				points.Add(base.NPC.Center + base.NPC.DirectionTo(ChainEndpoints[i]) * dist * 0.25f * (float)j);
			}
			points.Add(ChainEndpoints[i] + base.NPC.DirectionTo(ChainEndpoints[i]) * 18f);
			PrimitiveRenderer.RenderTrail(points, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction), 40);
		}
		return true;
	}

	public void TendrilDestructionEffects(int tendrilIndex)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 70; i++)
		{
			Dust dust = Dust.NewDustDirect(Vector2.Lerp(base.NPC.Center, ChainEndpoints[tendrilIndex], (float)i / 70f), 4, 4, 5);
			dust.velocity = Main.rand.NextVector2Circular(3f, 3f);
			dust.scale = Main.rand.NextFloat(1f, 1.4f);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Vector2 heartGoreVelocity = default(Vector2);
			for (int i = 1; i <= 2; i++)
			{
				((Vector2)(ref heartGoreVelocity))._002Ector((float)(i == 1).ToDirectionInt() * 3f, Main.rand.NextFloat(-2f, 0f));
				Gore.NewGorePerfect(base.NPC.GetSource_Death(), base.NPC.Center, heartGoreVelocity, base.Mod.Find<ModGore>($"BrimstoneHeart_Gore{i}").Type, base.NPC.scale);
			}
		}
		for (int j = 0; j < ChainEndpoints.Count; j++)
		{
			TendrilDestructionEffects(j);
		}
	}

	public override void DrawBehind(int index)
	{
		Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
	}

	public override bool CheckActive()
	{
		return false;
	}
}
