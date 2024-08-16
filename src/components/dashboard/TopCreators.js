import React from 'react';
import './TopCreators.css';
import manAvatar from './images/1.jpg';
import oldWoman from './images/2.jpg';
import blackMan from './images/3.jpg';
import businessMan from './images/4.jpg';
import beardedMan from './images/5.jpg';
import youngWoman from './images/6.jpg';
import latinaWoman from './images/7.jpg';

function TopCreators() {
    return (
        <section className="top-creators">
            <div className="header">
                <h2>Top Creators</h2>
                <a href="#" className="see-all">See all</a>
            </div>
            <div className="creators-list">
                <div className="creator">
                    <img src={manAvatar} alt="Creator 1" className="creator-img" />
                    <span className="name">@maddison_c21</span>
                    <span className="artworks">9821</span>
                    <span className="rating-bar"><progress value="80" max="100"></progress></span>
                </div>
                <div className="creator">
                    <img src={oldWoman} alt="Creator 2" className="creator-img" />
                    <span className="name">@karl.will02</span>
                    <span className="artworks">7032</span>
                    <span className="rating-bar"><progress value="70" max="100"></progress></span>
                </div>
                <div className="creator">
                    <img src={blackMan} alt="Creator 3" className="creator-img" />
                    <span className="name">@andreea.lz</span>
                    <span className="artworks">5204</span>
                    <span className="rating-bar"><progress value="60" max="100"></progress></span>
                </div>
                <div className="creator">
                    <img src={businessMan} alt="Creator 4" className="creator-img" />
                    <span className="name">@abraham47y</span>
                    <span className="artworks">4309</span>
                    <span className="rating-bar"><progress value="50" max="100"></progress></span>
                </div>
                <div className="creator">
                    <img src={beardedMan} alt="Creator 5" className="creator-img" />
                    <span className="name">@simmmple.web</span>
                    <span className="artworks">3871</span>
                    <span className="rating-bar"><progress value="40" max="100"></progress></span>
                </div>
                <div className="creator">
                    <img src={youngWoman} alt="Creator 6" className="creator-img" />
                    <span className="name">@horizon.ui</span>
                    <span className="artworks">3152</span>
                    <span className="rating-bar"><progress value="30" max="100"></progress></span>
                </div>
                <div className="creator">
                    <img src={latinaWoman} alt="Creator 7" className="creator-img" />
                    <span className="name">@venus.sys</span>
                    <span className="artworks">2907</span>
                    <span className="rating-bar"><progress value="20" max="100"></progress></span>
                </div>
            </div>
        </section>
    );
}

export default TopCreators;
