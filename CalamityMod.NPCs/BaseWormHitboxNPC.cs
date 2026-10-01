using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public abstract class BaseWormHitboxNPC : ModNPC
{
	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.NPC.width = 200;
		base.NPC.height = 200;
		base.NPC.lifeMax = 10000;
		base.NPC.knockBackResist = 0f;
		base.NPC.noTileCollide = true;
		base.NPC.aiStyle = -1;
	}

	public override void AI()
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		NPC headNPC = Main.npc[(int)base.NPC.ai[0]];
		if (!(headNPC.ModNPC is BaseWormNPC) || !headNPC.active)
		{
			base.NPC.active = false;
			return;
		}
		base.NPC.realLife = (int)base.NPC.ai[0];
		base.NPC.damage = headNPC.damage;
		base.NPC.lifeMax = headNPC.lifeMax;
		base.NPC.life = headNPC.life;
		base.NPC.Center = (headNPC.ModNPC as BaseWormNPC).Segments[(int)base.NPC.ai[1]].Center;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		return false;
	}
}
