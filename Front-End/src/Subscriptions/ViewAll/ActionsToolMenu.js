import React, { useRef, useState } from 'react';
import {
    IconButton,
    ListItemIcon,
    ListItemText,
    Menu,
    MenuItem,
    Toolbar,
    Tooltip,
} from '@material-ui/core';
import DescriptionIcon from '@material-ui/icons/DescriptionOutlined';
import EditIcon from '@material-ui/icons/EditOutlined';
import BlockIcon from '@material-ui/icons/BlockOutlined';
import MoreIcon from '@material-ui/icons/MoreHoriz';

function ActionsToolMenu(){
    const moreRef = useRef(null);
    const [openMenu, setOpenMenu] = useState(false);

    const handleMenuOpen = () => {
        setOpenMenu(true);
    };

    const handleMenuClose = () => {
        setOpenMenu(false);
    };

    return(
        <Toolbar>
            <Tooltip title="More options">
                <IconButton
                    onClick={handleMenuOpen}
                    ref={moreRef}
                >
                    <MoreIcon />
                </IconButton>
            </Tooltip>
            <Menu
                anchorEl={moreRef.current}
                keepMounted
                elevation={1}
                onClose={handleMenuClose}
                open={openMenu}
            >
                <MenuItem>
                    <ListItemIcon>
                        <EditIcon />
                    </ListItemIcon>
                    <ListItemText primary="Edit & Configure" />
                </MenuItem>
                <MenuItem>
                    <ListItemIcon>
                        <DescriptionIcon />
                    </ListItemIcon>
                    <ListItemText primary="Details" />
                </MenuItem>
                <MenuItem>
                    <ListItemIcon>
                        <BlockIcon />
                    </ListItemIcon>
                    <ListItemText primary="Cancel Subscription" />
                </MenuItem>
            </Menu>
        </Toolbar>
    );
}

export default ActionsToolMenu;