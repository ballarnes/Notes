import { observer } from 'mobx-react'
import React, { Suspense } from 'react'
import { Container, Spinner, Tab, Tabs } from 'react-bootstrap'
import { useInjection } from '../../ioc/ioc.react'
import ownTypes from '../../ioc/ownTypes'
import HomePageStore, { TabsType } from '../../stores/HomePageStore'
import { useTranslation } from 'react-i18next';
import { ButtonGroup } from 'react-bootstrap'
import { Button } from 'react-bootstrap'
import { Row } from 'react-bootstrap'
import { Col } from 'react-bootstrap'


const Note = React.lazy(() => import('../../containers/Note'))
const Notes = React.lazy(() => import('../../containers/Notes'))
const CreateNote = React.lazy(() => import('../../containers/CreateNote'))

const HomePage = observer(() => {
  const store = useInjection<HomePageStore>(ownTypes.homePageStore);
  const { t, i18n } = useTranslation();
  const changeLanguage = (lng: string) => {
    i18n.changeLanguage(lng);
  }
  return (
    <Suspense fallback={<Spinner animation="border" />}>
      <Container className="pt-4 pb-4 justify-content-center">
      <Row className="justify-content-center">
        <Col lg={1} md={1}>
        <ButtonGroup aria-label="Basic example" className="pb-4 justify-content-center">
          <Button variant="secondary" onClick={() => changeLanguage('ru')}>RU</Button>
          <Button variant="secondary" onClick={() => changeLanguage('en')}>EN</Button>
        </ButtonGroup>
          </Col></Row>
        <Tabs
          activeKey={store.currentTab}
          onSelect={(ev)=> {store.changeTab(ev)}}
          className="mb-3"
        >
          <Tab eventKey={TabsType[TabsType.Note]} title={t('tabs.note')}>
            {store.currentTab === `${TabsType[TabsType.Note]}` && <Note />}
          </Tab>
          <Tab eventKey={TabsType[TabsType.Notes]} title={t('tabs.notes')}>
            {store.currentTab === `${TabsType[TabsType.Notes]}` && <Notes />}
          </Tab>
          <Tab eventKey={TabsType[TabsType.CreateNote]} title={t('tabs.createNote')}>
            {store.currentTab === `${TabsType[TabsType.CreateNote]}` && <CreateNote/>}
          </Tab>
          
        </Tabs>
      </Container>
    </Suspense>
  )
});

export default HomePage;
