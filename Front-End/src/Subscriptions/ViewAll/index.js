import '../../App.css'

import React from 'react';
import DataTable from "react-data-table-component";
import Chip from '@material-ui/core/Chip';
import ActionsToolMenu from './ActionsToolMenu';

const data = [
    { id: 1, canRedeem: 'False', level: "Vip ++", status: "Active", year: '1982' },
    { id: 2, canRedeem: 'True', level: "Vip",status: "Active", year: '2042' }
];
const columns = [
    {
        name: 'End Date',
        selector: 'endDate',
        sortable: true
    },
    {
        name: 'Start Date',
        selector: 'startDate',
        sortable: true,
    },
    {
        name: 'SteamId 64',
        selector: 'steamId',
        sortable: true,
    },
    {
        name: 'Level',
        selector: 'level',
        sortable: true,
    },
    {
        name: 'Status',
        selector: 'status',
        sortable: true,
        cell: row => <div>
            <Chip
                color="primary"
                variant="outlined"
                size="small"
                label={row.status}
            /></div>
    },
    {
        name: 'Can Redeem',
        selector: 'canRedeem',
        sortable: true
    },
    {
        name: "Actions",
        selector: "actions",
        cell: row => <div>
            <ActionsToolMenu variant="contained" color="primary"> Hello World </ActionsToolMenu>
        </div>,
    }
];

function SubscriptionsList() {

    return (
        <DataTable
            title={"Movie Name"}
            columns={columns}
            data={data}
            selectableRows
        />
    );
}

export default SubscriptionsList;