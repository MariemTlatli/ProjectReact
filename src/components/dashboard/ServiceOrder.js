import React from 'react';
import './ServiceOrder.css';

function ServiceOrder() {
    return (
        
        <section class="service-order">
        <h2>Service Order</h2>
        <div class="table">
            <div class="table-header">
                <h3>Complex Table</h3>
                <i class="fas fa-ellipsis-h"></i>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Status</th>
                        <th>Date</th>
                        <th>Progress</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Horizon UI PRO</td>
                        <td><span class="status approved">Approved</span></td>
                        <td>18 Apr 2022</td>
                        <td><progress value="80" max="100"></progress></td>
                    </tr>
                    <tr>
                        <td>Horizon UI Free</td>
                        <td><span class="status disabled">Disable</span></td>
                        <td>18 Apr 2022</td>
                        <td><progress value="50" max="100"></progress></td>
                    </tr>
                    <tr>
                        <td>Marketplace</td>
                        <td><span class="status error">Error</span></td>
                        <td>20 May 2021</td>
                        <td><progress value="30" max="100"></progress></td>
                    </tr>
                    <tr>
                        <td>Weekly Updates</td>
                        <td><span class="status approved">Approved</span></td>
                        <td>12 Jul 2021</td>
                        <td><progress value="90" max="100"></progress></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </section>
    );

}
export default ServiceOrder;
