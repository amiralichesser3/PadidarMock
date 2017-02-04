using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using MadarshoMockServer.Context;
using MadarshoMockServer.Models;
using MadarshoMockServer.Utility;

namespace MadarshoMockServer.Controllers
{
    public class MockController : ApiController
    {
        [HttpPost]
        [Route("api/mock/profile/login")]
        public IHttpActionResult Login()
        {
            return Ok();
        }

        [HttpPost]
        [Route("api/mock/profile/info")]
        public IHttpActionResult Info(long timestamp)
        { 
            using (var db = new SnapshotContext())
            {
                var userSnapShot = db.UserSnapshots.FirstOrDefault();
                if (userSnapShot != null)
                { 
                    PurpleUser purpleUser = userSnapShot.GetPurpleUser(); 
                    if (purpleUser.timestamp > timestamp)
                    {
                        return Ok(purpleUser);
                    }

                    return Ok();
                }

                return BadRequest("no snapshots");
            }
        }

        [HttpPost]
        [Route("api/mock/profile/update")]
        public IHttpActionResult UpdateProfile(UserChange userChange)
        {
            if (userChange.user == null && userChange.removedUser == null)
            {
                return BadRequest("both cannot be null");
            }

            using (var db = new SnapshotContext())
            {
                var snapshot = db.UserSnapshots.First();
                var currentUser = snapshot.GetPurpleUser();
                if (currentUser == null)
                {
                    return BadRequest("profile does not exist");
                }

                var finalResult = currentUser;
                if (userChange.user != null)
                {
                    finalResult = merge(currentUser, userChange.user);
                }
                if (userChange.removedUser != null)
                {
                    remove(finalResult, userChange.removedUser);
                }

                db.UserSnapshots.Remove(snapshot);
                finalResult.timestamp = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
                UserSnapshot userSnapshot = new UserSnapshot(finalResult);
                db.UserSnapshots.Add(userSnapshot);
                db.SaveChanges();
                return Ok(new UpdateProfileResponse(finalResult.timestamp));
            } 
        }

        private void remove(PurpleUser currentUser, PurpleUser removedUser)
        {
            if (removedUser == null)
            {
                return;
            }

            //periods
            if (removedUser.periods != null && currentUser.periods != null)
            {
                List<int> removeeList = new List<int>();
                foreach (var removee in removedUser.periods)
                { 
                    int removeCounter = -1;
                    for (int i = 0; i < currentUser.periods.Count; i++)
                    {
                        if (myEquals(currentUser.periods[i], removee))
                        {
                            removeCounter = i;
                            break;
                        }
                    }

                    if (removeCounter != -1)
                    {
                        removeeList.Add(removeCounter);
                    }
                }

                foreach (int index in removeeList)
                {
                    currentUser.periods.RemoveAt(index);
                }
            }


            //weights
            if (removedUser.weights != null && currentUser.weights != null)
            {
                foreach (var removee in removedUser.weights)
                {
                    int removeCounter = -1;
                    for (int i = 0; i < currentUser.weights.Count; i++)
                    {
                        if (currentUser.weights[i].date.Equals(removee.date) && currentUser.weights[i].weight == removee.weight)
                        {
                            removeCounter = i;
                            break;
                        }
                    }

                    if (removeCounter != -1)
                    {
                        currentUser.weights.RemoveAt(removeCounter);
                    }
                }
            }

            //prenatalCares
            if (removedUser.prenatalCares != null && currentUser.prenatalCares != null)
            {
                foreach (var removee in removedUser.prenatalCares)
                {
                    int removeCounter = -1;
                    for (int i = 0; i < currentUser.prenatalCares.Count; i++)
                    {
                        if (currentUser.prenatalCares[i].date.Equals(removee.date) && currentUser.prenatalCares[i].prenatalCareId == removee.prenatalCareId)
                        {
                            removeCounter = i;
                            break;
                        }
                    }

                    if (removeCounter != -1)
                    {
                        currentUser.prenatalCares.RemoveAt(removeCounter);
                    }
                }
            }

            //symptoms
            if (removedUser.symptoms != null && currentUser.symptoms != null)
            {
                foreach (var removee in removedUser.symptoms)
                {
                    int removeCounter = -1;
                    for (int i = 0; i < currentUser.symptoms.Count; i++)
                    {
                        if (currentUser.symptoms[i].date.Equals(removee.date) && currentUser.symptoms[i].symptomId == removee.symptomId)
                        {
                            removeCounter = i;
                            break;
                        }
                    }

                    if (removeCounter != -1)
                    {
                        currentUser.symptoms.RemoveAt(removeCounter);
                    }
                }
            }
        }

    private bool myEquals(PurpleDate p1, PurpleDate p2)
    {
        if (p1.day != p2.day)
        {
            return false;
        }

        if (p1.month != p2.month)
        {
            return false;
        }

        if (p1.year != p2.year)
        {
            return false;
        }

        return true;
    }

    private PurpleUser merge(PurpleUser userChanges1, PurpleUser userChanges2)
        {
            PurpleUser result = new PurpleUser();
             
            //firstname
            if (userChanges2.firstName != null)
            {
                result.firstName = userChanges2.firstName;
            }
            else if (userChanges1.firstName != null)
            {
                result.firstName = userChanges1.firstName;
            }

            //lastname
            if (userChanges2.lastName != null)
            {
                result.lastName = userChanges2.lastName;
            }
            else if (userChanges1.lastName != null)
            {
                result.lastName = userChanges1.lastName;
            }

            //birthday
            if (userChanges2.birthday != null)
            {
                result.birthday = userChanges2.birthday;
            }
            else if (userChanges1.birthday != null)
            {
                result.birthday = userChanges1.birthday;
            }

            //fcmToken
            if (userChanges2.fcmToken != null)
            {
                result.fcmToken = userChanges2.fcmToken;
            }
            else if (userChanges1.fcmToken != null)
            {
                result.fcmToken = userChanges1.fcmToken;
            }

            //height
            if (userChanges2.height != null)
            {
                result.height = userChanges2.height;
            }
            else if (userChanges1.height != null)
            {
                result.height = userChanges1.height;
            }

            //state
            if (userChanges2.state != null)
            {
                result.state = userChanges2.state;
            }
            else if (userChanges1.state != null)
            {
                result.state = userChanges1.state;
            }

            //lastPeriod
            if (userChanges2.lastPeriod != null)
            {
                result.lastPeriod = userChanges2.lastPeriod;
            }
            else if (userChanges1.lastPeriod != null)
            {
                result.lastPeriod = userChanges1.lastPeriod;
            }

            //newsletter
            if (userChanges2.newsletter != null)
            {
                result.newsletter = userChanges2.newsletter;
            }
            else if (userChanges1.newsletter != null)
            {
                result.newsletter = userChanges1.newsletter;
            }

            //pregnancy
            if (userChanges2.pregnancy != null)
            {
                result.pregnancy = userChanges2.pregnancy;
            }
            else if (userChanges1.pregnancy != null)
            {
                result.pregnancy = userChanges1.pregnancy;
            }

            //ttc
            if (userChanges2.ttc != null)
            {
                result.ttc = userChanges2.ttc;
            }
            else if (userChanges1.ttc != null)
            {
                result.ttc = userChanges1.ttc;
            } 
                    
            result.weights = new List<PurpleUserWeight>();
            result.periods = new List<PurpleDate>();
            result.prenatalCares = new List<PurpleUserPrenatalCare>();
            result.symptoms = new List<PurpleUserSymptom>();
            //weights
            result.weights = new List<PurpleUserWeight>();
            if (userChanges1.weights != null)
            {
                result.weights.AddRange(userChanges1.weights);
                result.weights = result.weights.Distinct().ToList();
            }
            if (userChanges2.weights != null)
            {
                result.weights.AddRange(userChanges2.weights);
                result.weights = result.weights.Distinct().ToList();
            }

            //periods 
            if (userChanges1.periods != null)
            {
                result.periods.AddRange(userChanges1.periods);
                result.periods = result.periods.Distinct().ToList();
            }
            if (userChanges2.periods != null)
            {
                result.periods.AddRange(userChanges2.periods);
                result.periods = result.periods.Distinct().ToList();
            }

            //prenatalCares 
            if (userChanges1.prenatalCares != null)
            {
                result.prenatalCares.AddRange(userChanges1.prenatalCares);

                result.prenatalCares = result.prenatalCares.Distinct().ToList();
            }
            if (userChanges2.prenatalCares != null)
            {
                result.prenatalCares.AddRange(userChanges2.prenatalCares);

                result.prenatalCares = result.prenatalCares.Distinct().ToList();
            }

            //symptoms 
            if (userChanges1.symptoms != null)
            {
                result.symptoms.AddRange(userChanges1.symptoms);
                result.symptoms = result.symptoms.Distinct().ToList();

            }
            if (userChanges2.symptoms != null)
            {
                result.symptoms.AddRange(userChanges2.symptoms);
                result.symptoms = result.symptoms.Distinct().ToList();
            } 

            return result;
        }

        [HttpPost]
        [Route("api/mock/profile/content")]
        public IHttpActionResult Content()
        {
            using (var db = new SnapshotContext())
            {
                var contentSnapshot = db.ContentSnapshots.FirstOrDefault();
                if (contentSnapshot != null)
                {
                    return Ok(contentSnapshot.serializedText);
                }

                return Ok();
            }
        }

        [HttpPost]
        [Route("api/mock/profile/postcontent")]
        public IHttpActionResult PostContent(PurpleContent purpleContent)
        {
            using (var db = new SnapshotContext())
            {
                var contentSnapshot = db.ContentSnapshots.FirstOrDefault();
                if (contentSnapshot != null)
                {
                    db.ContentSnapshots.Remove(contentSnapshot);
                }

                db.ContentSnapshots.Add(new ContentSnapshot(purpleContent));
                db.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        [Route("api/mock/profile/postwebsitechange")]
        public IHttpActionResult PostWebsiteChange(UserChange userChange)
        {
            using (var db = new SnapshotContext())
            {
                var currentSnapshot = db.UserSnapshots.First();
                
                if (currentSnapshot != null)
                {
                    var currentUser = currentSnapshot.GetPurpleUser();
                    PurpleUser newUser = merge(currentUser, userChange.user);
                    db.UserSnapshots.Remove(currentSnapshot);
                    UserSnapshot userSnapshot = new UserSnapshot(newUser);
                    db.UserSnapshots.Add(userSnapshot);
                    db.SaveChanges();
                    new PushNotification(new SyncObject("syncUser", userChange, newUser.timestamp), newUser.fcmToken);
                }

                return Ok();
            }
        }
    }
}