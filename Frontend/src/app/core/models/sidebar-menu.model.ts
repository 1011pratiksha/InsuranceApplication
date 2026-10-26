/**
 * Represents a single item in the sidebar navigation.
 * Items can be nested to form a tree structure with collapsible sub-menus.
 */
export interface SidebarMenuItem {
  /** Display label shown in the sidebar */
  label: string;

  /** Material icon name (uses Material Symbols Rounded) */
  icon?: string;

  /** Router link path – leaf items must have this */
  route?: string;

  /** Nested children – when present the item acts as an expandable group */
  children?: SidebarMenuItem[];

  /** Optional badge count (e.g. pending items) */
  badge?: number;
}

/** Supported user roles */
export type UserRole = 'customer' | 'agent' | 'admin';
