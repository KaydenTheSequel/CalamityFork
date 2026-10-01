using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class BloodwormFleeing : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.BloodwormNormal.DisplayName");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 12;
		base.NPC.height = 42;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 5;
		base.NPC.knockBackResist = 0f;
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit20;
		base.NPC.DeathSound = SoundID.NPCDeath12;
	}

	public override void AI()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[Player.FindClosest(base.NPC.Center, 1, 1)];
		if (base.NPC.velocity == Vector2.Zero)
		{
			base.NPC.velocity = Vector2.UnitY * 12f;
		}
		float intertia = 24f;
		base.NPC.velocity = (base.NPC.velocity * intertia - base.NPC.SafeDirectionTo(player.Center) * 12f) / (intertia + 1f);
		base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
		base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 5.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= Main.npcFrameCount[base.Type] * frameHeight)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
	}
}
